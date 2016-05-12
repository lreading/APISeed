using System.Configuration;

namespace APISeed.BusinessLayer.Email
{
    internal static class ClientConfig
    {
        internal class CredentialStore
        {
            public static CredentialStore Instance = new CredentialStore();
            static CredentialStore() { }
            private const string _HostKey = "SmtpHost";
            private const string _PortKey = "SmtpPort";
            private const string _UsernameKey = "SmtpUserName";
            private const string _PasswordKey = "SmtpPassword";
            private CredentialStore()
            {
                Host = ConfigurationManager.AppSettings[_HostKey];
                Port = int.Parse(ConfigurationManager.AppSettings[_PortKey]);
                UserName = ConfigurationManager.AppSettings[_UsernameKey];
                Password = ConfigurationManager.AppSettings[_PasswordKey];
            }

            public string Host { get; private set; }
            public int Port { get; private set; }
            public string UserName { get; private set; }
            public string Password { get; private set; }
        }
    }
}
