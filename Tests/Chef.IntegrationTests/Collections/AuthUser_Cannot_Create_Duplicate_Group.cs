using Chef.Collections;
using Chef.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chef.IntegrationTests.Collections
{
    /// <summary>
    /// A collection name cannot be duplicated.
    /// </summary>
    [TestClass]
    public class AuthUser_Cannot_Create_Duplicate_Group
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
        public void ProfileService_AuthUser_Cannot_Create_Duplicate_Group()
        {
            _group = _service.CreateGroup(Actor.AUTH_USER, "A Testing Group 你好");
            var dupGroup = _service.CreateGroup(Actor.AUTH_USER, "A Testing Group 你好");

            Assert.IsNull(dupGroup);
        }
    }
}
