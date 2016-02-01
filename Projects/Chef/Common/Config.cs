namespace Chef
{
    /// <summary>
    /// Configure your system.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Path of your email template in web application.
        /// </summary>
        public const string EMAIL_TEMPLATE_PATH = "~/app_data/emails.xml";
        /// <summary>
        /// UserName can only contain "a-z,A-Z,0-9".
        /// </summary>
        public const string USERNAME_REGEX = @"^[a-zA-Z0-9]+$";
        /// <summary>
        /// We allow username up to 20 chars.
        /// </summary>
        /// <remarks>
        /// Facebook 50 http://stackoverflow.com/questions/8078939/what-is-the-maximum-length-of-a-facebook-name
        /// Twitter 15 http://support.twitter.com/entries/14609-how-to-change-your-username
        /// </remarks>
        public const int USERNAME_MAXLENGTH = 20;
        /// <summary>
        /// We allow username no less than 2 chars.
        /// </summary>
        public const int USERNAME_MINLENGTH = 2; 
        /// <summary>
        /// "anonymous"
        /// </summary>
        public const string ANONYMOUS_USERNAME = "anonymous";
        /// <summary>
        /// Usernames cannot be one of the following.
        /// </summary>
        /// <remarks>
        /// Since UserName_MinLength is at 2, all reserved terms need to cover 2-char words too if any.
        /// </remarks>
        public static string[] USERNAME_RESERVED = new string[] { 
            "admin", "anonymous", "api", "account", "about", "all", "ad", "ads",
            "blog", "blogs", 
            "contact", "classifieds", "career", "careers",
            "developers", "discuss", "discussion", "discussions", 
            "forum", "forums", 
            "home","help",
            "jobs",
            "login", "logout", "list", "lists", 
            "manage", "message", "messages", "market", "marketplace", 
            "people", "privacy", "photo", "photos", 
            "qna", "question", "questions", 
            "recipes", "recipe", "register", "restaurant", "restaurants", 
            "system", "social", "space", "spaces", "stars",
            "terms",
            "user", "users",
            "wiki", "wikis", 
            "video", "videos",
            "signin-google"
            };
        /// <summary>
        /// Default groups created for each new user.
        /// </summary>
        public static string[] DEFAULT_GROUPS = new string[] { "Favorite", "Family" };
    }
}
