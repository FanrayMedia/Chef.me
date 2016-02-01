using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Chef.Profiles;
using Chef.Collections;

namespace Chef.IntegrationTests.Collections
{
    /// <summary>
    /// An authenticated user can add and remove another user to or from his collections.
    /// </summary>
    [TestClass]
    public class AuthUser_Can_Add_Remove_A_Contact_To_Group
    {
        ProfileService _service;
        Group<Profile> _group = null;
        string _groupName = "New Group";
        string _groupSlug = "new-group";
        int _targetUserId = 0;

        [TestInitialize]
        public void SetUp()
        {
            _service = new ProfileService();
            TestUtils.Create_Auth_User();
            TestUtils.Create_Target_User();
            _targetUserId = _service.GetProfile(Actor.TARGET_USER).Id;
        }

        [TestCleanup]
        public void CleanUp()
        {
            _service.DeleteGroup(Actor.AUTH_USER, _group.Id);
            TestUtils.Delete_Target_User();
            TestUtils.Delete_Auth_User();
        }

        [TestMethod]
        public void ProfileService_AuthUser_Can_Add_Remove_A_Contact_To_Group()
        {
            // Given a group
            _group = _service.CreateGroup(Actor.AUTH_USER, _groupName);

            // When auth user adds a new user to one of his collections
            _service.AddRemoveItemToGroup(Actor.AUTH_USER, _targetUserId, _group.Id);

            // Then the user is added to the collection
            var group = _service.GetGroup(Actor.AUTH_USER, _groupSlug);
            Assert.IsTrue(group.Items.Any(i => i.Id == _targetUserId));

            // When auth user removes the user from the collection
            _service.AddRemoveItemToGroup(Actor.AUTH_USER, _targetUserId, _group.Id);

            // The the user is removed from the collection
            group = _service.GetGroup(Actor.AUTH_USER, _groupSlug);
            Assert.IsFalse(group.Items.Any(i => i.Id == _targetUserId));
        }
    }
}
