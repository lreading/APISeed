using System.Configuration;
using System.Net.Mail;

namespace APISeed.BusinessLayer.Email
{
    /// <summary>
    /// The base class for all emailing methods
    /// </summary>
    public abstract class BaseEmail
    {
        /// <summary>
        /// The name of the application.  Backing field for lazy loading and persistence
        /// </summary>
        private static string _applicationName;

        /// <summary>
        /// The name of the application.  Lazy loaded from the appSettings (ApplicationName)
        /// </summary>
        public static string ApplicationName
        {
            get
            {
                return string.IsNullOrWhiteSpace(_applicationName) ? _applicationName : ConfigurationManager.AppSettings["ApplicationName"];
            }
        }

        /// <summary>
        /// Gets an SMTP Client based on the app settings with info provided by the ClientConfig.CredentialStore singleton
        /// </summary>
        /// <returns></returns>
        public SmtpClient GetClient()
        {
            var client = new SmtpClient(ClientConfig.CredentialStore.Instance.Host, ClientConfig.CredentialStore.Instance.Port);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ClientConfig.CredentialStore.Instance.UserName, ClientConfig.CredentialStore.Instance.Password);
            return client;
        }
    }
}
