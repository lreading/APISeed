namespace APISeed.BusinessLayer.Email
{
    /// <summary>
    /// Email message templates
    /// </summary>
    internal static class MessageTemplates
    {
        /// <summary>
        /// Sent after a user signs up.
        /// </summary>
        /// <param name="confirmationUrl"></param>
        /// <returns></returns>
        public static string ConfirmRegistration(string confirmationUrl)
        {
            return string.Format("<h2>Thanks!</h2><br /><br /><p>We're excited to have you!</p><p>Please confirm your account by clicking <a href=\"{0}\">here</a></p>", confirmationUrl);
        }

        /// <summary>
        /// Sent when a user requests that their password be reset.
        /// </summary>
        /// <param name="passwordResetUrl"></param>
        /// <returns></returns>
        public static string ResetPassword(string passwordResetUrl)
        {
            return string.Format("<h2>Forgot your password?</h2><br /><br /><p>Hey, we were noobs at one point too...  We'll forgive you this time.</p><p>To reset your password, click <a href=\"{0}\">here</a></p>", passwordResetUrl);
        }
    }
}
