using Chef.Collections;
using Chef.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chef.IntegrationTests.Collections
{
    /// <summary>
    /// An authenticated user can create collections.
    /// </summary>
    [TestClass]
    public class AuthUser_Can_Create_New_Group
    {
        ProfileService _service;
        Group<Profile> _group = null;

        [TestInitialize]
        public void SetUp()
        {
            _service = new ProfileService();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _service.DeleteGroup(Actor.AUTH_USER, _group.Id);
        }

        [TestMethod]
        public void ProfileService_AuthUser_Can_Create_New_Group()
        {
            _group = _service.CreateGroup(Actor.AUTH_USER, "A Testing Group 你好");
            Assert.AreNotEqual(0, _group.Id);
        }
    }
}
