using Chef.Profiles;
using Chef.Web.Models.Profiles;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Chef.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// I switched from web api to mvc controller returning json the last minute before publishing, 
    /// the reason is complicated, all I can say is web api will be back in the next version.
    /// </remarks>
    public class ProfileController : ChefMvcController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CollectionsUsers()
        {
            return View();
        }

        // -------------------------------------------------------------------- get/update profile

        #region GET api/profile/{userName}

        /// <summary>
        /// Returns a user profile with styles by userName or 404 if no profile found.
        /// </summary>
        public ActionResult GetProfile(string userName)
        {
            Profile profile = _profileService.GetProfile(userName, includeStyles: true);
            if (profile == null) return new HttpNotFoundResult();
            return Json(ProfileVM.Map(profile));
        }

        #endregion

        #region [Authorize] POST api/profile/updateBgImg

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// </param>
        [Authorize]
        [HttpPost]
        public ActionResult UpdateBgImg(UpdateBgImgIM model)
        {
            _profileService.UpdateBgImg(_currentUserName, model.ImgFileName);
            return new HttpStatusCodeResult(200); 
        }

        #endregion

        #region [Authorize] POST api/profile/updateCoordinates

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// </param>
        [Authorize]
        [Route("updateCoordinates")]
        public ActionResult UpdateCoordinates(UpdateCoordinatesIM model)
        {
            _profileService.UpdateCoordinates(_currentUserName, model.X, model.Y);
            return new HttpStatusCodeResult(200);
        }

        #endregion

        #region [Authorize] POST api/profile/update

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// </param>
        [Authorize]
        [Route("update")]
        public ActionResult UpdateProfile(UpdateProfileIM model)
        {
            Profile profile = JsonConvert.DeserializeObject<Profile>(model.ProfileField);
            _profileService.UpdateProfile(_currentUserName, profile);
            return new HttpStatusCodeResult(200);
        }

        #endregion

        #region [Authorize] POST api/profile/updateDesign

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// </param>
        [Authorize]
        [Route("updateDesign")]
        public ActionResult UpdateDesign(UpdateDesignIM model)
        {
            ProfileStyle profileStyle = JsonConvert.DeserializeObject<ProfileStyle>(model.DesignField);

            _profileService.UpdateDesign(_currentUserName, profileStyle);
            return new HttpStatusCodeResult(200);
        }

        #endregion

        // -------------------------------------------------------------------- addMe

        #region [Authorized] GET api/profile/grouplist_with_target/{targetId}

        /// <summary>
        /// Returns group list for the authenticated user with the TargetExists 
        /// property marked to true if targetId exists in a group.  This endpoint
        /// is only accessible by authenticated user.
        /// </summary>
        /// <param name="targetId">profileId</param>
        /// <returns></returns>
        [Authorize]
        [Route("grouplist_with_target/{targetId}")]
        public ActionResult GetGroupListWithTarget(int targetId)
        {
            return Json(_profileService.GetGroupListWithTarget(_currentUserName, targetId).Select(g => GroupVM.Map(g)));
        }

        #endregion

        #region [Authorize] POST api/profile/groups/create_with_target

        /// <summary>
        /// Allows the authenticated user to create a new group while adding a 
        /// user to the new group.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The new group or bad request if creation failed.</returns>
        [Authorize]
        [Route("create_group_with_target")]
        public ActionResult CreateGroupWithTarget(CreateGroupWithTargetIM model)
        {
            var group = _profileService.CreateGroupWithTarget(_currentUserName, model.NewGroupName, model.TargetId);
            if (group == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Response.StatusCode = 200;
            return Json(GroupVM.Map(group));
        }

        #endregion

        #region [Authorize] POST api/profile/add_remove_user_to_group

        /// <summary>
        /// Allows the authenticated user to remove a user from a group.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("add_remove_user_to_group")]
        public ActionResult AddRemoveUserToGroup(AddRemoveUserToGroupIM model)
        {
            _profileService.AddRemoveItemToGroup(_currentUserName, model.TargetId, model.GroupId);
            return new HttpStatusCodeResult(200);
        }

        #endregion

        // -------------------------------------------------------------------- collectionsUsers

        #region GET api/{userName}/group/{groupSlug?}

        /// <summary>
        /// Returns the group with collected users or 404 if no group found.
        /// Returns all collected users when groupSlug is emtpy.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupSlug"></param>
        /// <returns></returns>
        /// <remarks>
        /// For "Optional parameters and default values", see
        /// http://aspnetwebstack.codeplex.com/wikipage?title=Attribute+routing+in+Web+API
        /// http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#optional
        /// basically param followed by ? and must have a default value.
        /// Therefore, "/api/people/ray/group" will return all contacts.
        /// </remarks>
        public ActionResult GetGroup(string userName, string groupSlug = "")
        {
            var group = _profileService.GetGroup(userName, groupSlug);
            if (group == null) return new HttpNotFoundResult();
            return Json(GroupVM.Map(group));
        }

        #endregion


        #region GET api/profile/{userName}/grouplist

        /// <summary>
        /// Returns all groups the user has created for collecting other user 
        /// profiles.  Empty list is returned if no groups has been created.
        /// </summary>
        /// <param name="userName"></param>
        [Route("{userName}/grouplist")]
        public ActionResult GetGroupList(string userName)
        {
            return Json(_profileService.GetGroupList(userName).Select(g => GroupVM.Map(g)));
        }

        #endregion

        #region [Authorize] POST api/profile/create_group

        /// <summary>
        /// Allows the authenticated user to create a new group.
        /// </summary>
        /// <param name="model">
        /// </param>
        /// <returns>The new group or bad request if creation failed.</returns>
        [Authorize]
        [Route("create_group")]
        public ActionResult CreateGroup(CreateGroupIM model)
        {
            var group = _profileService.CreateGroup(_currentUserName, model.NewGroupName);
            if (group == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Response.StatusCode = 200;
            return Json(GroupVM.Map(group));
        }

        #endregion

        #region [Authorize] POST api/profile/update_group

        /// <summary>
        /// Allows the authenticated user to update an existing group's info such as group name.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("update_group")]
        public ActionResult UpdateGroup(UpdateGroupIM model)
        {
            var newSlug = _profileService.UpdateGroup(_currentUserName, model.GroupId, model.NewGroupName);
            Chef.Collections.Group<Profile> updatedGroup = _profileService.GetGroup(_currentUserName, newSlug);

            Response.StatusCode = 200;
            return Json(GroupVM.Map(updatedGroup));
        }

        #endregion

        #region [Authorize] POST api/profile/destroy_group

        /// <summary>
        /// Allows the authenticated user to delete an existing group, by doing so the contacts
        /// in that groups
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// {"msg": "Update successful.", "group_deleted": "test2", "success": true, "error": 0}
        /// </returns>
        [Authorize]
        [Route("destroy_group")]
        public ActionResult DestroyGroup(DestroyGroupIM model)
        {
            _profileService.DeleteGroup(_currentUserName, model.GroupId);
            return new HttpStatusCodeResult(200);
        }

        #endregion

        // -------------------------------------------------------------------- emailMe

        #region [Authorize] POST api/profile/emailme

        /// <summary>
        /// Allows the authenticated user to email another user.
        /// </summary>
        /// <param name="model"></param>
        [Authorize]
        [Route("emailme")]
        public ActionResult EmailMe(EmailMeIM model)
        {
            _profileService.EmailMe(_currentUserName, model.UserName, model.Message);
            return new HttpStatusCodeResult(200);
        }

        #endregion
    }
}