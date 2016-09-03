using Dapper;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data;
using System.Linq;

namespace APISeed.DataLayer.Schema
{
    /// <summary>
    /// Handles updating the schema when necessary
    /// </summary>
    public sealed class Startup
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(Startup));
        private readonly Interfaces.IConnectionFactory _connectionFactory;
        private readonly bool _isTest = false;
        public string UserId { get; set; }

        public Startup()
        {
            _connectionFactory = new ConnectionFactory();
        }

        public Startup(Interfaces.IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _isTest = true;
        }

        private IDbConnection _connection
        {
            get
            {
                return _connectionFactory.GetConnection();
            }
        }

        /// <summary>
        /// Checks conditions and pre-processes as necessary
        /// 
        /// Throws: Database Inaccessible Exception
        ///         InvalidSchemaVersionException
        /// </summary>
        public void Init()
        {
            try
            {
                // The update scrips count should always equal the current schema version.
                // If it does not, rectify it.  If it is higher than the update scripts version,
                // something is wrong and we must throw an invalid schema version exception.
                var currentSchemaVersion = GetDatabaseSchemaVersion();
                if (currentSchemaVersion < Scripts.UpdateScripts.Count)
                {
                    // Set up the identity tables if this is a fresh database
                    if (currentSchemaVersion == 0)
                    {
                        ConfigureIdentityTables();
                    }

                    // Update the schema starting with the appropriate index
                    for (var i = currentSchemaVersion; i < Scripts.UpdateScripts.Count; i++)
                    {
                        RunSchemaUpdateScript(i);
                    }
                }
                else if (currentSchemaVersion > Scripts.UpdateScripts.Count)
                {
                    _log.ErrorFormat("Schema version is higher than application code.  Current Application Code Schema: {0} - Current Database Schmea: {1}",
                        Scripts.UpdateScripts.Count, currentSchemaVersion);
                    throw new ApplicationException("Database schema version is higher than code base version.");
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex);
                throw;
            }
        }

        private void ConfigureIdentityTables()
        {
            if (_isTest)
            {
                using (var db = _connection)
                {
                    db.Execute(_identitySqlForTest);
                    CreateUserForTest(db);
                }
                return;
            }
            var context = new Models.ApplicationDbContext();
            var UserManager = new UserManager<Models.ApplicationUser>(new UserStore<Models.ApplicationUser>(context));
            var user = new Models.ApplicationUser() { UserName = "test@applicationally.com", Email = "test@applicationally.com" };
            user.Token = new Domain.Auth.BearerTokenModel();

            // The user will be deleted immediate, so the strength of the password and it being in plaintext should
            // be a relatively low risk.  Alternatively, you could use System.Web.Security.Membership.CreatePassword,
            // however, that is not guaranteed to meet the password strength requirements, which would then throw an error.
            var result = UserManager.Create(user, "alkASDFlkja23894902@#$Q90asdfm!");
            UserManager.Delete(user);
        }

        private void CreateUserForTest(IDbConnection conn)
        {
            var userId = Guid.NewGuid().ToString();
            conn.Execute(@"
INSERT INTO         AspNetUsers
VALUES              (@userId,
                        NULL,
	                    NULL,
                        NULL,
	                    NULL,
                        NULL,
                        NULL,
	                    NULL,
	                    0,
	                    NULL,
                        NULL,
                        NULL,
	                    0,
                        0,
                        NULL,
	                    0,
                        0,
	                    'TestUser');
", new { userId = userId });
            UserId = userId;
        }

        /// <summary>
        /// Mock identity table for testing purposes only
        /// </summary>
        private const string _identitySqlForTest = @"CREATE TABLE AspNetUsers(
	Id nvarchar(128) NOT NULL PRIMARY KEY,
	Token_access_token nvarchar(256) NULL,
	Token_token_type nvarchar(256) NULL,
	Token_expires_in bigint NULL,
	Token_UserName nvarchar(256) NULL,
	Token_issued datetime NULL,
	Token_expires datetime NULL,
	Email nvarchar(256) NULL,
	EmailConfirmed bit NOT NULL,
	PasswordHash nvarchar(256) NULL,
	SecurityStamp nvarchar(256) NULL,
	PhoneNumber nvarchar(256) NULL,
	PhoneNumberConfirmed bit NOT NULL,
	TwoFactorEnabled bit NOT NULL,
	LockoutEndDateUtc datetime NULL,
	LockoutEnabled bit NOT NULL,
	AccessFailedCount int NOT NULL,
	UserName nvarchar(256) NOT NULL
);

CREATE TABLE        AspNetRoles
(
    Id NVARCHAR(128) NOT NULL PRIMARY KEY,
    Name NVARCHAR(MAX) NOT NULL
);

";
        /// <summary>
        /// Gets the database schema version.  Returns 0 if it does not exist.
        /// </summary>
        /// <returns></returns>
        private int GetDatabaseSchemaVersion()
        {
            var schemaVersion = 0;
            if (SchemaExists())
            {
                schemaVersion = GetSchemaVersion();
            }
            return schemaVersion;
        }

        /// <summary>
        /// Checks the schema version against the count of the schema updates collection to determine if an update is required.
        /// </summary>
        /// <remarks>
        /// Throws ApplicationException if it cannot connect to the database using the DefaultConnection connection string as defined in the web.config
        /// </remarks>
        /// <returns></returns>
        private bool SchemaExists()
        {
            bool schemaExists = false;
            try
            {
                using (var db = _connection)
                {
                    schemaExists = db.Query<bool>(@"
SELECT				CASE 
						WHEN		OBJECT_ID('dbo._SchemaVersion') IS NOT NULL 
						THEN		1 
						ELSE		0 
					END
").FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to connect to sql database using the default connection string set in the web.config.  Please ensure that there is a connection string named \"Default Connection\" with the appropriate permissions.", ex);
            }
            return schemaExists;
        }

        /// <summary>
        /// Gets the database's current schema version.  
        /// Check to see that the schema is set up before calling this method.
        /// </summary>
        /// <returns></returns>
        private int GetSchemaVersion()
        {
            using (var db = _connection)
            {
                return db.Query<int>(@"
SELECT              Version
FROM                _SchemaVersion
").FirstOrDefault();
            }
        }

        /// <summary>
        /// Runs an update script based on the index of the script to run.
        /// Rather than accepting raw text to run as a sql script, we are adding
        /// another layer of control by enforcing the use of the Scripts object
        /// </summary>
        /// <param name="SchemaUpdateScriptIndex">The index of the schema update script to run</param>
        /// <returns></returns>
        private void RunSchemaUpdateScript(int SchemaUpdateScriptIndex)
        {
            var UpdateScripts = Scripts.UpdateScripts[SchemaUpdateScriptIndex];
            using (var db = _connection)
            {
                // Scripts are lists of strings to allow for the separation of batch executions
                foreach (var script in UpdateScripts)
                {
                    db.Execute(script);
                }
            }
        }
    }
}
