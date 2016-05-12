using System.Net.Mail;
using System.Threading.Tasks;

namespace APISeed.BusinessLayer.Email
{
    /// <summary>
    /// Sends emails to users regarding their account
    /// </summary>
    public class Account : BaseEmail
    {
        /// <summary>
        /// Sends the email confirmation after a user registers for the site.
        /// </summary>
        /// <param name="confirmationUrl"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public Task SendEmailConfirmationAsync(string confirmationUrl, string userEmail)
        {
            var client = GetClient();
            var message = new MailMessage(ClientConfig.CredentialStore.Instance.UserName, userEmail);
            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                message.Dispose();
            };

            message.IsBodyHtml = true;
            message.Body = MessageTemplates.ConfirmRegistration(confirmationUrl);
            message.Subject = ApplicationName + " - Account Confirmation";
            return client.SendMailAsync(message);
        }

        /// <summary>
        /// Sends the user an email with a link to change their password.
        /// </summary>
        /// <param name="confirmationUrl"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public Task SendForgotPasswordAsync(string confirmationUrl, string userEmail)
        {
            var client = GetClient();
            var message = new MailMessage(ClientConfig.CredentialStore.Instance.UserName, userEmail);
            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                message.Dispose();
            };

            message.IsBodyHtml = true;
            message.Body = MessageTemplates.ResetPassword(confirmationUrl);
            message.Subject = ApplicationName + " - Password Reset";
            return client.SendMailAsync(message);
        }
    }
}
