using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Security.Principal;

namespace Chef.Common.UnitTests
{
    /// <summary>
    /// Unit tests for SiteContext
    /// </summary>
    [TestClass]
    public class SiteContextTests
    {
        string identityName = "ray";
        Mock<HttpContextBase> MockContext = new Mock<HttpContextBase>();
        Mock<IPrincipal> MockPrincipal = new Mock<IPrincipal>();
        Mock<IIdentity> MockIdentity = new Mock<IIdentity>();

        /// <summary>
        /// SiteContext.Current.UserName returns IIdentity.Name.
        /// </summary>
        [TestMethod]
        public void SiteContext_Current_UserName_Returns_IIdentity_Name()
        {
            // Setup
            MockIdentity.Setup(x => x.Name).Returns(identityName); // IIdentity.Name
            MockPrincipal.Setup(x => x.Identity).Returns(MockIdentity.Object); // IPrincipal.IIdentity
            MockContext.Setup(x => x.User).Returns(MockPrincipal.Object); // HttpContextBase.User
            SiteContext.Current.Context = MockContext.Object; // set to the MockContext to our Context

            // Test
            Assert.AreEqual(identityName, SiteContext.Current.UserName);
        }

        /// <summary>
        /// SiteContext.Current.UserName returns "", when IIdentity.Name is null or empty.
        /// </summary>
        [TestMethod]
        public void SiteContext_Current_UserName_Returns_Anonymous_When_IIdentity_Name_Is_NullorEmtpy()
        {
            // Setup
            MockIdentity.Setup(x => x.Name).Returns(""); // IIdentity.Name
            MockPrincipal.Setup(x => x.Identity).Returns(MockIdentity.Object); // IPrincipal.IIdentity
            MockContext.Setup(x => x.User).Returns(MockPrincipal.Object); // HttpContextBase.User
            SiteContext.Current.Context = MockContext.Object; // set to the MockContext to our Context

            // Test
            Assert.AreEqual(Config.ANONYMOUS_USERNAME, SiteContext.Current.UserName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SiteContext_Current_IsAnonymous_Returns_False_When_Mocked_With_Value()
        {
            // Setup
            MockIdentity.Setup(x => x.Name).Returns(identityName); // IIdentity.Name
            MockPrincipal.Setup(x => x.Identity).Returns(MockIdentity.Object); // IPrincipal.IIdentity
            MockContext.Setup(x => x.User).Returns(MockPrincipal.Object); // HttpContextBase.User
            SiteContext.Current.Context = MockContext.Object; // set to the MockContext to our Context

            // Test
            Assert.AreEqual(false, SiteContext.Current.IsAnonymous);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SiteContext_Current_IsWebRequest_Returns_True_When_Mocked_With_Value()
        {
            // Setup
            SiteContext.Current.Context = MockContext.Object; // set to the MockContext to our Context

            // Test
            Assert.AreEqual(true, SiteContext.Current.IsWebRequest);
        }
    }
}
