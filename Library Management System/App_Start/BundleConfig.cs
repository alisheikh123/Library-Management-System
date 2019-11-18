using System.Web;
using System.Web.Optimization;

namespace Library_Management_System
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Bundles/css")
            .Include("~/Content/css/bootstrap.min.css")
            .Include("~/Content/css/bootstrap-select.css")
            .Include("~/Content/css/bootstrap-datepicker3.min.css")
            .Include("~/Content/css/font-awesome.min.css")
            .Include("~/Content/css/icheck/blue.min.css")
            .Include("~/Content/css/AdminLTE.css")
            .Include("~/Content/Magicsuggest/magicsuggest-min.css")
                 .Include("~/Content/Magicsuggest/magicsuggest.css")
 .Include("~/Content/datatable/datatables.css")
               .Include("~/Content/datatable/datatables.min.css")
            .Include("~/Content/css/skins/skin-blue.css"));

            bundles.Add(new ScriptBundle("~/Bundles/js")
           .Include("~/Content/js/plugins/jquery/jquery-3.3.1.js")
           .Include("~/Content/js/plugins/bootstrap/bootstrap.js")
           .Include("~/Content/js/plugins/fastclick/fastclick.js")
           .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
           .Include("~/Content/js/plugins/bootstrap-select/bootstrap-select.js")
           .Include("~/Content/js/plugins/moment/moment.js")
           .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
           .Include("~/Content/js/plugins/icheck/icheck.js")
           .Include("~/Content/js/plugins/validator.js")
                            .Include("~/Content/Magicsuggest/magicsuggest-min.js")
                 .Include("~/Content/Magicsuggest/magicsuggest.js")

           .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
           .Include("~/Content/js/adminlte.js")
                .Include("~/Content/datatable/datatables.js")
            .Include("~/Content/datatable/datatables.min.js")

           .Include("~/Content/js/init.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                       "~/Content/table.css",
                      "~/Content/form.css"));
        }
    }
}
