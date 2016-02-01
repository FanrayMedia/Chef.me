using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chef.Profiles;

namespace Chef.IntegrationTests.Profiles
{
    /// <summary>
    /// User cannot register with an email that already exists.
    /// </summary>
    [TestClass]
    public class Registration_Does_Not_Allow_Duplicate_Email
    {
        ProfileService _service;
        Profile _profile;

        [TestInitialize]
        public void SetUp()
        {
            _service = new ProfileService();
            _profile = new Profile()
            {
                UserName = Actor.AUTH_USER,
                Email = "tom@notset.com",
                FirstName = "Tom",
                LastName = "Cat",
                Locations = "Pasadena, CA, USA",
                Headline = "American chef, author, and television personality",
                Bio = "He is a cat chef.",
                BgImg = "A02.jpg",
                ProfileStyle = ProfileStyle.GetDefault(),
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            _service.DeleteProfile(Actor.AUTH_USER);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ProfileService_Registration_Does_Not_Allow_Duplicate_Email()
        {
            // create a new user
            var result = _service.CreateUserProfile(_profile);
            Assert.IsTrue(result.Succeeded);

            // create a new user with different username but same email
            _profile.UserName = "NewUsername";

            result = _service.CreateUserProfile(_profile);
            Assert.IsFalse(result.Succeeded);
        }
    }
}
