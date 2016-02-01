using Chef.Profiles;

namespace Chef.Web.Models.Profiles
{
    /// <summary>
    /// Full profile shown on "/{userName}" page.
    /// </summary>
    public class ProfileVM : UserVM
    {
        public ProfileStyle Styles { get; set; }
        public string Headline { get; set; }
        public string Bio { get; set; }
        public string Locations { get; set; }
        public bool AllowEmailMe { get; set; }

        /// <summary>
        /// The "A01" part of "A01.jpg".
        /// </summary>
        public string BgImgName { get; set; }
        /// <summary>
        /// The ".jpg" part of "A01.jpg", starts with ".".
        /// </summary>
        public string BgImgExt { get; set; }

        public bool ShowEditProfile
        {
            get
            {
                return SiteContext.Current.Context.Request.IsAuthenticated &&
                    this.UserName.ToLower() == SiteContext.Current.UserName.ToLower();
            }
        }

        public new static ProfileVM Map(Profile profile)
        {
            return new ProfileVM { 
                Id = profile.Id, // important
                BgImg = profile.BgImg,
                BgImgName = profile.BgImg.Split('.')[0],
                BgImgExt = "." + profile.BgImg.Split('.')[1],

                UserName = profile.UserName,
                FirstName = profile.FirstName,
                LastName = profile.LastName,

                Styles = profile.ProfileStyle,
                Headline = profile.Headline,
                Bio = profile.Bio,
                Locations = profile.Locations,
                AllowEmailMe = profile.AllowEmailMe,
            };
        }
    }
}