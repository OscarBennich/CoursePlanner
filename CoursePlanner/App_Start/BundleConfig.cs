using System.Web;
using System.Web.Optimization;

namespace CoursePlanner
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Css").Include(
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/dist/css/alt/AdminLTE-select2.min.css",
                      "~/Content/dist/css/AdminLTE.min.css",
                      "~/Content/dist/css/skins/skin-red-light.min.css",
                      "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Content/plugins/datatables/dataTables.bootstrap.css",
                      "~/Content/TermCourseOverview.css"
                      ));

            bundles.Add(new ScriptBundle("~/Js").Include(
                     "~/Content/plugins/jQuery/jquery-2.2.3.min.js",
                     "~/Content/plugins/jQueryUI/jquery-ui.min.js",
                     "~/Content/bootstrap/js/bootstrap.min.js",
                     "~/Content/plugins/datatables/jquery.dataTables.min.js",
                     "~/Content/plugins/datatables/dataTables.bootstrap.min.js",
                     "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                     "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",
                     "~/Content/plugins/fastclick/fastclick.js",
                     "~/Content/plugins/chartjs/Chart.js",
                     "~/Content/plugins/chartjs/Chart.min.js",
                     "~/Content/dist/js/app.min.js",
                     "~/Content/CourseConflictFunctions.js",
                     "~/Content/plugins/select2/select2.full.min.js"
                     ));



            BundleTable.EnableOptimizations = true;

        }
    }
}