using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chef.Profiles;

namespace Chef.IntegrationTests.Profiles
{
    /// <summary>
    /// User cannot register with a username that is a reserved word in the system.
    /// </summary>
    [TestClass]
    public class Registration_Does_Not_Allow_Reserved_Username
    {
        ProfileService _service;
        Profile _profile;

        [TestInitialize]
        public void SetUp()
        {
            _service = new ProfileService();
            _profile = new Profile()
            {
                UserName = "admin", // Note "admin" is a reserved username
                Email = "admin@notset.com",
                FirstName = "admin",
                LastName = "admin",
                Locations = "",
                Headline = "",
                Bio = "",
                BgImg = "A02.jpg",
                ProfileStyle = ProfileStyle.GetDefault(),
            };
        }

        [TestMethod]
        public void ProfileService_Registration_Does_Not_Allow_Reserved_Username()
        {
            var result = _service.CreateUserProfile(_profile);
            Assert.IsFalse(result.Succeeded);
        }
    }
}
