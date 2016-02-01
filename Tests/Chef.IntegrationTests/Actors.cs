namespace Chef.IntegrationTests
{
    /// <summary>
    /// Different people that may interact with our systems, used exlusively by
    /// testing projects.
    /// </summary>
    public class Actor
    {
        /// <summary>
        /// "tom" the user who is logged.
        /// </summary>
        public const string AUTH_USER = "tom";
        /// <summary>
        /// "jerry" the user whose profile is being visited, or who is being added as 
        /// a contact by another user.
        /// </summary>
        public const string TARGET_USER = "jerry";
        /// <summary>
        /// An anonymous user visiting.
        /// </summary>
        public const string ANONYMOUS_USER = Config.ANONYMOUS_USERNAME;
    }
}
