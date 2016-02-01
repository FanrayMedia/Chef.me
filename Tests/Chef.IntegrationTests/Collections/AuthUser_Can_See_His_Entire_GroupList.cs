using Chef.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chef.IntegrationTests.Collections
{
    /// <summary>
    /// All groups a user creates for collecting other users are public.
    /// </summary>
    [TestClass]
    public class AuthUser_Can_See_His_Entire_GroupList
    {
        ProfileService _service;
        [TestInitialize]
        public void SetUp()
        {
            _service = new ProfileService();
        }

        [TestMethod]
        public void ProfileService_AuthUser_Can_See_His_Entire_GroupList()
        {
            var groupList = _service.GetGroupList(Actor.AUTH_USER);
            Assert.IsNotNull(groupList);
        }
    }
}
