using Chef.Web.Models.Profiles;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Chef.Web.Controllers
{
    public class PeopleController : ChefMvcController
    {
        // GET: People
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// http://localhost:18232/people/all
        /// </summary>
        /// <returns></returns>
        public ActionResult All()
        {
            List<UserVM> users = new List<UserVM>();
            var profiles = _profileService.GetAllProfiles();
            foreach (var profile in profiles)
            {
                users.Add(UserVM.Map(profile));
            }

            return Json(users);
        }
    }
}