using Chef.Profiles;
using Microsoft.AspNet.Identity;

namespace Chef.IntegrationTests
{
    /// <summary>
    /// Some utility methods assisting integration tests.
    /// </summary>
    public static class TestUtils
    {
        /// <summary>
        /// Creates the authenticated user.
        /// </summary>
        public static IdentityResult Create_Auth_User()
        {
            ProfileService service = new ProfileService();

            return service.CreateUserProfile(new Profile
            {
                UserName = Actor.AUTH_USER,
                Email = Actor.AUTH_USER + "@notset.com",
                FirstName = "Tom",
                LastName = "Cat",
                Locations = "San Gabriel, CA, USA",
                ProfileStyle = ProfileStyle.GetDefault()
            });
        }

        /// <summary>
        /// Deletes the authenticated user.
        /// </summary>
        public static void Delete_Auth_User()
        {
            ProfileService service = new ProfileService();
            service.DeleteProfile(Actor.AUTH_USER);
        }

        /// <summary>
        /// Creates the target user.
        /// </summary>
        public static IdentityResult Create_Target_User()
        {
            ProfileService service = new ProfileService();

            return service.CreateUserProfile(new Profile
            {
                UserName = Actor.TARGET_USER,
                Email = Actor.TARGET_USER + "@notset.com",
                FirstName = "Jerry",
                LastName = "Mouse",
                Locations = "Pasadena, CA, USA",
                ProfileStyle = ProfileStyle.GetDefault()
            });
        }

        /// <summary>
        /// Deletes the target user.
        /// </summary>
        public static void Delete_Target_User()
        {
            ProfileService service = new ProfileService();
            service.DeleteProfile(Actor.TARGET_USER);
        }

    }
}
