using System.Configuration;

namespace $safeprojectname$.Email
{
    /// <summary>
    /// Email client related configurations
    /// </summary>
    internal static class ClientConfig
    {
        /// <summary>
        /// Singleton instance of the credentials.  Lazy loaded and only reads the config once.
        /// </summary>
        internal class CredentialStore
        {
            // Singleton implementation
            public static CredentialStore Instance = new CredentialStore();
            static CredentialStore() { }
            private const string _HostKey = "SmtpHost";
            private const string _PortKey = "SmtpPort";
            private const string _UsernameKey = "SmtpUserName";
            private const string _PasswordKey = "SmtpPassword";
            private CredentialStore()
            {
                // Get the values from the app settings
                Host = ConfigurationManager.AppSettings[_HostKey];
                Port = int.Parse(ConfigurationManager.AppSettings[_PortKey]);
                UserName = ConfigurationManager.AppSettings[_UsernameKey];
                Password = ConfigurationManager.AppSettings[_PasswordKey];
            }

            /// <summary>
            /// The email provider host address
            /// </summary>
            public string Host { get; private set; }

            /// <summary>
            /// The port on which to send emails (typically 25)
            /// </summary>
            public int Port { get; private set; }

            /// <summary>
            /// The username for the account you will use to send emails
            /// </summary>
            public string UserName { get; private set; }

            /// <summary>
            /// The password for the account you will use to send emails
            /// </summary>
            public string Password { get; private set; }
        }
    }
}
