using DataLayer.Repositories;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SimpleInjector;
using System;

namespace DataLayer
{
    /// <summary>
    /// Registers types for the data layer
    /// </summary>
    public static class IocRegistration
    {
        /// <summary>
        /// Registers types
        /// </summary>
        /// <param name="container"></param>
        public static void Register(Container container, IAppBuilder app)
        {
            AuthReg(container, app);
            container.Register<IConnectionFactory, ConnectionFactory>(Lifestyle.Scoped);
            // ex: container.Register<IAsyncRepository<WidgetModel>, WidgetRepository>(Lifestyle.Scoped);
        }

        private static void AuthReg(Container container, IAppBuilder app)
        {
            var userStoreReg = Lifestyle.Scoped.CreateRegistration<Auth.UserStore>(container);
            container.AddRegistration(typeof(Auth.UserStore), userStoreReg);
            container.AddRegistration(typeof(IUserStore<Auth.ApplicationUser, int>), userStoreReg);

            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<Auth.ApplicationUser>(Lifestyle.Scoped);

            container.Register<Auth.ApplicationUserManager>(Lifestyle.Scoped);
            container.RegisterInitializer<Auth.ApplicationUserManager>(manager => InitializeUserManager(manager, app));

            container.Register<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>(Lifestyle.Scoped);
            container.Register<ITextEncoder, Base64UrlTextEncoder>(Lifestyle.Scoped);
            container.Register<IDataSerializer<AuthenticationTicket>, TicketSerializer>(Lifestyle.Scoped);
            container.Register(() => new DpapiDataProtectionProvider().Create("ASP.NET Identity"), Lifestyle.Scoped);
        }


        /// <summary>
        /// Initializes the User Manager
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        private static Auth.ApplicationUserManager InitializeUserManager(Auth.ApplicationUserManager manager, IAppBuilder app)
        {
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<Auth.ApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = app.GetDataProtectionProvider();
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<Auth.ApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
