using System.Web.Mvc;
using System.Web.Routing;

namespace Chef.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // ---------------------------------------------------------------- ignore

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // ---------------------------------------------------------------- home

            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" });
            routes.MapRoute("About", "about", new { controller = "Home", action = "About" });
            routes.MapRoute("Contact", "contact", new { controller = "Home", action = "Contact" });
            routes.MapRoute("Terms", "terms", new { controller = "Home", action = "Terms" });
            routes.MapRoute("Privacy", "privacy", new { controller = "Home", action = "Privacy" });

            // ---------------------------------------------------------------- account

            routes.MapRoute("Login", "login", new { controller = "Account", action = "Login" });
            routes.MapRoute("LogOff", "logoff", new { controller = "Account", action = "LogOff" });
            routes.MapRoute("Manage", "account/manage", new { controller = "Manage", action = "Index" });

            // ---------------------------------------------------------------- people

            routes.MapRoute("People", "people", new { controller = "People", action = "Index" });

            // ---------------------------------------------------------------- profile api

            routes.MapRoute("UpdateBgImg", "api/profile/updateBgImg", new { controller = "Profile", action = "UpdateBgImg"});
            routes.MapRoute("UpdateCoordinates", "api/profile/updateCoordinates", new { controller = "Profile", action = "UpdateCoordinates" });
            routes.MapRoute("UpdateProfile", "api/profile/update", new { controller = "Profile", action = "UpdateProfile" });
            routes.MapRoute("UpdateDesign", "api/profile/updateDesign", new { controller = "Profile", action = "UpdateDesign" });
            routes.MapRoute("GetGroupListWithTarget", "api/profile/grouplist_with_target/{targetId}", new { controller = "Profile", action = "GetGroupListWithTarget", targetId = 0 });
            routes.MapRoute("CreateGroupWithTarget", "api/profile/create_group_with_target", new { controller = "Profile", action = "CreateGroupWithTarget" });
            routes.MapRoute("AddRemoveUserToGroup", "api/profile/add_remove_user_to_group", new { controller = "Profile", action = "AddRemoveUserToGroup" });
            routes.MapRoute("CreateGroup", "api/profile/create_group", new { controller = "Profile", action = "CreateGroup" });
            routes.MapRoute("UpdateGroup", "api/profile/update_group", new { controller = "Profile", action = "UpdateGroup" });
            routes.MapRoute("DestroyGroup", "api/profile/destroy_group", new { controller = "Profile", action = "DestroyGroup" });
            routes.MapRoute("EmailMe", "api/profile/emailme", new { controller = "Profile", action = "EmailMe" });
            routes.MapRoute("GetGroup", "api/profile/{userName}/group/{groupSlug}", new { controller = "Profile", action = "GetGroup", userName = "", groupSlug = UrlParameter.Optional });
            routes.MapRoute("GetGroupList", "api/profile/{userName}/grouplist", new { controller = "Profile", action = "GetGroupList", userName = "" });
            routes.MapRoute("GetProfile", "api/profile/{userName}", new { controller = "Profile", action = "GetProfile", userName = "" });

            // ---------------------------------------------------------------- profile

            routes.MapRoute("Profile", "{userName}", new { controller = "Profile", action = "Index", userName = "" });
            routes.MapRoute("CollectionsUsers", "{userName}/collections/{groupSlug}",
                new { controller = "Profile", action = "CollectionsUsers", userName = "", groupSlug = "" });

            // ---------------------------------------------------------------- default

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}