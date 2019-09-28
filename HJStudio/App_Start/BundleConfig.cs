using System.Web;
using System.Web.Optimization;

namespace HJStudio
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     
                      "~/Content/JavaScript/scripts.bundle.js",
                      "~/Content/JavaScript/vendors.bundle.js",
                       "~/Content/JavaScript/datatables.bundle.js",
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/StyleSheet/datatables.bundle.css",
                      "~/Content/StyleSheet/style.bundle.css",
                      "~/Content/StyleSheet/vendors.bundle.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/assets/vendors/base/vendorsJs").Include(
                     "~/assets/vendors/base/vendors.bundle.js",
                     "~/assets/demo/demo6/base/scripts.bundle.js",
                      "~/assets/vendors/custom/datatables/datatables.bundle.js",
                      "~/assets/vendors/custom/fullcalendar/fullcalendar.bundle.js",
                      "~/Content/JavaScript/basic.js",
                      "~/Content/JavaScript/bootstrap-datepicker.js"
                      ));

            bundles.Add(new StyleBundle("~/assets/vendors/base/vendorsCss").Include(
                      "~/assets/vendors/base/vendors.bundle.css",
                      "~/assets/demo/demo6/base/style.bundle.css",
                      "~/assets/vendors/custom/datatables/datatables.bundle.css",
                      "~/assets/vendors/custom/fullcalendar/fullcalendar.bundle.css",
                      "~/Content/site.css"));

        }
    }
}
