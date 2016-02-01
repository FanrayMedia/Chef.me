using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chef.Common.UnitTests
{
    /// <summary>
    /// Unit tests for WebHelper.
    /// </summary>
    [TestClass]
    public class WebHelperTests
    {
        [TestMethod]
        public void FormatSlug()
        {
            Assert.AreEqual("ill-love-asp-net-i-doesnt-like-it-now", WebHelper.FormatSlug(", I'll love asp.net, I doesn't like it now-."));
            Assert.AreEqual("dont", WebHelper.FormatSlug("don’t "));
            Assert.AreEqual("doesnt", WebHelper.FormatSlug("doesn’t "));

            Assert.AreEqual("my-1-0-release", WebHelper.FormatSlug("my 1.0 release"));
            Assert.AreEqual("my-10-release", WebHelper.FormatSlug("my 10 release"));

            Assert.AreEqual("c", WebHelper.FormatSlug("c#"));
            Assert.AreEqual("c", WebHelper.FormatSlug("c"));

            Assert.AreEqual("11-2", WebHelper.FormatSlug("1+1=2"));
            Assert.AreEqual("112", WebHelper.FormatSlug("1>1>2"));

            Assert.AreEqual("1-1-1-1", WebHelper.FormatSlug("1.1-1_1_.-"));

            Assert.AreEqual("", WebHelper.FormatSlug("你好。"));
        }

    }
}
