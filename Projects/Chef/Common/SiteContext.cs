using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace Chef
{
    /// <summary>
    /// SiteContext unifies HttpContext and HttpContextBase.  It wraps around an HttpContextBase 
    /// and it's used instead of HttpContext and HttpContextBase in the system. 
    /// </summary>
    public class SiteContext
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        private SiteContext()
        {
        }

        /// <summary>
        /// Init this.Context and this.Server
        /// </summary>
        /// <param name="context"></param>
        /// <param name="server"></param>
        private SiteContext(HttpContextBase context, HttpServerUtilityBase server)
        {
            this.Context = context;
            this.Server = server;
        }

        #endregion

        #region Current

        [ThreadStatic]
        private static SiteContext _siteContext = null;

        /// <summary>
        /// Returns a instance of SiteContext.
        /// </summary>
        public static SiteContext Current
        {
            get
            {
                string dataKey = "ChefSiteContext";

                if (HttpContext.Current != null)
                {
                    HttpContextBase httpContextBase = new HttpContextWrapper(HttpContext.Current);

                    if (httpContextBase.Items.Contains(dataKey))
                    {
                        return httpContextBase.Items[dataKey] as SiteContext;
                    }
                    else
                    {
                        SiteContext siteContext = new SiteContext(httpContextBase, new HttpServerUtilityWrapper(HttpContext.Current.Server)); 
                        httpContextBase.Items[dataKey] = siteContext;
                        return siteContext;
                    }
                }
                else if (_siteContext == null)
                {
                    _siteContext = new SiteContext();
                }

                return _siteContext;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Unload()
        {
            _siteContext = null;
        }

        #endregion

        #region Server (HttpServerUtilityBase)

        /// <summary>
        /// HttpServerUtilityBase object. This property is null for non-web-requests.
        /// </summary>
        public HttpServerUtilityBase Server
        {
            get;
            set;
        }

        #endregion

        #region Context (HttpContextBase)

        /// <summary>
        /// HttpContextBase object.  This property is null for non-web-requests.
        /// </summary>
        public HttpContextBase Context
        {
            get;
            set; // public set here for mocking purpose
        }

        #endregion

        #region Items

        private HybridDictionary _items = new HybridDictionary();

        /// <summary>
        /// Returns a per request storage bag, generally expect 10 or less items.
        /// </summary>
        public IDictionary Items
        {
            get { return _items; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return this.Items[key]; }
            set { this.Items[key] = value; }
        }

        #endregion

        // -------------------------------------------------------------------- Properties

        #region UserName

        /// <summary>
        /// Returns either currently logged on user's UserName or "anonymous" for non-logged on users.
        /// </summary>
        /// <remarks>
        /// If request is not authenticated the Context.User.Identity.Name will return null, 
        /// but here I return "anonymous".
        /// </remarks>
        public string UserName
        {
            get
            {
                if (this.Context != null && !string.IsNullOrWhiteSpace(this.Context.User.Identity.Name))
                {
                    return this.Context.User.Identity.Name;
                }
                else
                {
                    return Config.ANONYMOUS_USERNAME;
                }
            }
        }

        #endregion

        #region IsAnonymous

        /// <summary>
        /// Is request anonymous
        /// </summary>
        /// <remarks>
        /// Becomes false when mocked, this helps 
        /// </remarks>
        public bool IsAnonymous
        {
            get
            {
                return (this.Context == null || string.IsNullOrEmpty(this.Context.User.Identity.Name));
            }
        }

        #endregion

        #region IsWebRequest

        /// <summary>
        /// True if this.Current != null. 
        /// </summary>
        /// <remarks>
        /// In unit testing if you mock it, this will return true.
        /// </remarks>
        public bool IsWebRequest
        {
            get { return this.Context != null; }
        }

        #endregion
    }
}
