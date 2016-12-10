using System.Collections.Generic;

namespace DataLayer.Startup
{
    /// <summary>
    /// Class to hold our schema related scripts.
    /// All scripts should be versioned, and the _SchemaVersion should be incremented at the end of the script.
    /// Dapper will choke on "GO", so we are using lists for separate transactions if necessary.
    /// After adding a script, add the reference to the UpdateScripts variable
    /// </summary>
    internal static class Scripts
    {
        #region Scripts

        /// <summary>
        /// Create the _SchemaVersion table
        /// </summary>
        internal static readonly List<string> V1 = new List<string>
        {
            @"
CREATE TABLE            _SchemaVersion
(
    Version INT PRIMARY KEY
);

INSERT INTO         _SchemaVersion
VALUES              (1);
",
            @"

INSERT INTO         AspNetRoles
VALUES              ('Administrator');
"
        };

        #endregion

        /// <summary>
        /// The total summation of all of the scripts
        /// </summary>
        internal static readonly IReadOnlyList<IReadOnlyList<string>> UpdateScripts = new List<List<string>>()
        {
            V1
        };
    }
}
