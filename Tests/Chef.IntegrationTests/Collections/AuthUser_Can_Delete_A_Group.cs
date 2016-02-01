using Chef.Collections;
using Chef.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chef.IntegrationTests.Collections
{
    /// <summary>
    /// An authenticated user can delete a collection.
    /// </summary>
    [TestClass]
    public class AuthUser_Can_Delete_A_Group
    {
        ProfileService _service;
        Group<Profile> _group = null;

        [TestInitialize]
        public void SetUp()
        {
            _service = new ProfileService();
            _group = _service.CreateGroup(Actor.AUTH_USER, "Test Group");
        }

        [TestMethod]
        public void ProfileService_AuthUser_Can_Delete_A_Group()
        {
            _service.DeleteGroup(Actor.AUTH_USER, _group.Id);
        }
    }
}
