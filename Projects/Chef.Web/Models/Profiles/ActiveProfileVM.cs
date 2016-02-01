using Chef.Profiles;
using System.Web;
using System.Web.Mvc;

namespace Chef.Web.Models.Profiles
{
    public class ActiveProfileVM
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }

        protected UrlHelper UrlHelper
        {
            get { return new UrlHelper(HttpContext.Current.Request.RequestContext); }
        }
        /// <summary>
        /// The absolute link to the user's profile. This is actually used by the _LoginPartial.cshtml
        /// </summary>
        public string ProfileUrl
        {
            get
            {
                return UrlHelper.RouteUrl("Profile", new { userName = UserName });
            }
        }

        public static ActiveProfileVM Map(Profile profile)
        {
            return (profile == null) ? null : new ActiveProfileVM
            {
                UserName = profile.UserName,
                FirstName = profile.FirstName,
            };
        }
    }
}