using System.Web.Optimization;

namespace Chef.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.css",
                "~/wwwroot/css/chef.css",
                "~/wwwroot/lib/font-awesome-4.5.0/css/font-awesome.css",
                "~/Content/toastr.css",
                "~/wwwroot/lib/angular-bootstrap-colorpicker/css/colorpicker.css",
                "~/wwwroot/lib/angular-xeditable-0.1.8/css/xeditable.css")
                .Include("~/wwwroot/lib/angular-awesome-slider/css/angular-awesome-slider.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                     "~/Scripts/angular.js",
                       // 3rd party modules 
                       "~/wwwroot/lib/angular-ui-bootstrap/ui-bootstrap-tpls-0.14.3.js", 
                       "~/wwwroot/lib/angular-xeditable-0.1.8/js/xeditable.js", 
                       "~/wwwroot/lib/angular-bootstrap-colorpicker/js/bootstrap-colorpicker-module.js",
                       "~/wwwroot/lib/angular-awesome-slider/angular-awesome-slider.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/misc").Include(
                   "~/Scripts/jquery-ui.js",
                   "~/Scripts/respond.js",
                   "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/wwwroot/app/app.js",
                "~/wwwroot/app/config.js",
                "~/wwwroot/app/common/common.js",
                "~/wwwroot/app/common/logger.js",
                "~/wwwroot/app/common/commonDialog.js", 
                "~/wwwroot/app/common/commonService.js",
                // people
                "~/wwwroot/app/people/usersController.js",
                "~/wwwroot/app/people/usersService.js",
                // profile
                "~/wwwroot/app/profile/registerController.js",
                "~/wwwroot/app/profile/profileController.js",
                "~/wwwroot/app/profile/profileService.js",
                "~/wwwroot/app/profile/profileDraggable.js",
                "~/wwwroot/app/profile/collectionsUsersController.js",
                "~/wwwroot/app/profile/profileCollectionService.js",
                "~/wwwroot/app/profile/addMe/addMeDialog.js",
                "~/wwwroot/app/profile/emailMe/emailMeDialog.js"
           ));
        }
    }
}
