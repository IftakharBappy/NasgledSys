using System.Web;
using System.Web.Optimization;

namespace NasgledSys
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Content/assets/js/plugins/loaders/pace.min.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Content/assets/js/plugins/loaders/pace.min.js",
                   "~/Content/assets/js/core/libraries/jquery.min.js",                  
                      "~/Content/assets/js/core/libraries/bootstrap.min.js",
                       "~/Content/assets/js/plugins/loaders/blockui.min.js",
                        "~/Content/assets/js/plugins/ui/nicescroll.min.js",
                         "~/Content/assets/js/plugins/ui/drilldown.js",
                           "~/Content/assets/js/core/libraries/jquery_ui/interactions.min.js",
                          "~/Content/assets/js/plugins/tables/datatables/datatables.min.js",

                           "~/Content/assets/js/plugins/forms/selects/select2.min.js",
                            "~/Content/assets/js/pages/form_select2.js",
                          "~/Content/assets/js/plugins/visualization/d3/d3.min.js",
                           "~/Content/assets/js/plugins/visualization/d3/d3_tooltip.js",
                            "~/Content/assets/js/plugins/forms/styling/switchery.min.js",
                             "~/Content/assets/js/plugins/forms/styling/uniform.min.js",
                              "~/Content/assets/js/plugins/forms/selects/bootstrap_multiselect.js",
                               "~/Content/assets/js/plugins/ui/moment/moment.min.js",
                               "~/Content/assets/js/plugins/pickers/daterangepicker.js",

                                
                                   "~/Content/assets/js/plugins/forms/styling/uniform.min.js",
                            "~/Content/assets/js/core/app.js",
                            "~/Content/assets/js/pages/form_layouts.js",
                              "~/Content/assets/js/plugins/ui/ripple.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/assets/css/icons/icomoon/styles.css",
                      "~/Content/assets/css/bootstrap.css",
                      "~/Content/assets/css/core.css",
                     "~/Content/assets/css/components.css",
                     "~/Content/assets/css/colors.css"
                      ));
          
        }
    }
}
