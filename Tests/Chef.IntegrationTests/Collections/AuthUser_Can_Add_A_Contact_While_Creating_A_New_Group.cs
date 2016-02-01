using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Chef.Profiles;
using Chef.Collections;

namespace Chef.IntegrationTests.Collections
{
    /// <summary>
    /// An authenticate user can create a new group and put another user into 
    /// the group at the same time.
    /// </summary>
    [TestClass]
    public class AuthUser_Can_Add_A_Contact_While_Creating_A_New_Group
    {
        ProfileService _service;
        Group<Profile> _group = null;
        string _groupName = "New Group"; // must be not already existing
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
        public void ProfileService_AuthUser_Can_Add_A_Contact_While_Creating_A_New_Group()
        {
            // Given auth user creates a new group and puts a user into the group
            _group = _service.CreateGroupWithTarget(Actor.AUTH_USER, _groupName, _targetUserId);

            // When we get the group
            var group = _service.GetGroup(Actor.AUTH_USER, _groupSlug);

            // Then that user is there
            Assert.IsTrue(group.Items.Any(i => i.UserName.Equals(Actor.TARGET_USER, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
