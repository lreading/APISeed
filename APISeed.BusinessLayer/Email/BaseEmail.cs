using System.Configuration;
using System.Net.Mail;

namespace APISeed.BusinessLayer.Email
{
    public abstract class BaseEmail
    {
        private static string _applicationName;
        public static string ApplicationName
        {
            get
            {
                return string.IsNullOrWhiteSpace(_applicationName) ? _applicationName : ConfigurationManager.AppSettings["ApplicationName"];
            }
        }
        public SmtpClient GetClient()
        {
            var client = new SmtpClient(ClientConfig.CredentialStore.Instance.Host, ClientConfig.CredentialStore.Instance.Port);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ClientConfig.CredentialStore.Instance.UserName, ClientConfig.CredentialStore.Instance.Password);
            return client;
        }
    }
}
