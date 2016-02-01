using Chef.Profiles;
using System.Web;
using System.Web.Mvc;

namespace Chef.Web.Models.Profiles
{
    public class UserVM
    {
        /// <summary>
        /// User profile Id.
        /// </summary>
        public int Id { get; set; }
        public string BgImg { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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

        public static UserVM Map(Profile profile)
        {
            return new UserVM
            {
                Id = profile.Id,
                BgImg = profile.BgImg,
                UserName = profile.UserName,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
            };
        }
    }
}