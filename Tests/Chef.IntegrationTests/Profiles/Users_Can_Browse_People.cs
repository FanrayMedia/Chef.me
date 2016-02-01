using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chef.Profiles;

namespace Chef.IntegrationTests.Profiles
{
    /// <summary>
    /// Any user can browse people's profiles on /people.
    /// </summary>
    [TestClass]
    public class Users_Can_Browse_People
    {
        ProfileService _service;

        [TestInitialize]
        public void SetUp()
        {
            _service = new ProfileService();
        }

        [TestMethod]
        public void ProfileService_Users_Can_Browse_People()
        {
            var users = _service.GetAllProfiles();

            Assert.IsNotNull(users);
        }
    }
}
