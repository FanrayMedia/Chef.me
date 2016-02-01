using Chef.Profiles;
using Chef.Web.Models.Profiles;
using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Chef.Web.Controllers
{
    /// <summary>
    /// Base controller.
    /// </summary>
    public class ChefMvcController : Controller
    {
        #region constructor

        protected ProfileService _profileService;
        /// <summary>
        /// The logged-on user name.
        /// </summary>
        protected string _currentUserName;

        /// <summary>
        /// Initializes ActiveProfile.
        /// </summary>
        /// <remarks>
        /// ActiveProfile is needed throughout the site and it comes from mvc controllers.
        /// </remarks>
        public ChefMvcController()
        {
            _profileService = new ProfileService();
            _currentUserName = SiteContext.Current.UserName;
            var activeProfileVM = ActiveProfileVM.Map(_profileService.GetProfile(_currentUserName));
            ViewBag.ActiveProfile = WebHelper.ToJson(activeProfileVM);
        }

        #endregion

        #region Errors

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        protected void AddErrors(ValidationResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.ErrorMessage);
            }
        }

        #endregion

        /// <summary>
        /// Returns json result in camel casing.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected new ActionResult Json(object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            if (Request.RequestType == WebRequestMethods.Http.Get && behavior == JsonRequestBehavior.DenyGet)
                throw new InvalidOperationException("GET is no permitted for this request");

            var jsonResult = new ContentResult
            {
                Content = WebHelper.ToJson(data), 
                ContentType = "application/json"
            };

            return jsonResult;
        }
    }

}