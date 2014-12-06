using System.Web;
using System.Web.Optimization;

namespace LoWD
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                      "~/Scripts/jquery/dist/jquery.min.js",
                      "~/Scripts/datatables//media/js/jquery.dataTables.min.js",
                      "~/Scripts/angular/angular.min.js",
                      "~/Scripts/angular-datatables/dist/angular-datatables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/tablesort").Include(
                      "~/Scripts/lib/jquery/dist/jquery.min.js",
                     "~/Scripts/lib/angular/angular.min.js",
                     "~/Scripts/lib/angular-tablesort/js/angular-tablesort.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/bootstrap.css",
                      "~/Content/animate.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/font-awesome.css",
                      "~/Content/isotrope.css",
                      "~/Content/overwrite.css",
                      "~/Content/style.css",
                      "~/Content/site.css",
                      "~/Content/tableSort.css"
                      ));
        }
    }
}
