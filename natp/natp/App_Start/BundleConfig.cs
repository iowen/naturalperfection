using System.Web;
using System.Web.Optimization;

namespace natp
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

            bundles.Add(new ScriptBundle("~/bundles/npJq2").Include(
          "~/Scripts/javascripts/jquery.tipsy.js",
          "~/Scripts/javascripts/jquery.carouFredSel-6.0.3-packed.js",
          "~/Scripts/javascripts/jquery.touchSwipe.min.js",
          "~/Plugins/plugins/titan/js/prettify.js",
          "~/Plugins/plugins/slider-revolution/jquery.themepunch.revolution.min.js",
          "~/Plugins/plugins/titan/js/jquery.titanlighbox.js",
                //    "~/Scripts/javascripts/lightbox.js",

                    "~/Scripts/javascripts/jquery.isotope.min.js",

          "~/Scripts/javascripts/app-head.js"));



            bundles.Add(new ScriptBundle("~/bundles/npJq").Include(
"~/Scripts/javascripts/foundation.min.js",
"~/Scripts/javascripts/modernizr.foundation.js",
"~/Scripts/javascripts/moment.js",
"~/Plugins/plugins/slider-revolution/jquery.themepunch.plugins.min.js",
"~/Plugins/plugins/slider-revolution/jquery.themepunch.revolution.min.js",
"~/Plugins/plugins/camera/scripts/jquery.mobile.customized.min.js",
"~/Plugins/plugins/camera/scripts/jquery.easing.1.3.js",
"~/Scripts/javascripts/jPages.min.js",
"~/Scripts/javascripts/jquery.uploadify.js",
"~/Scripts/javascripts/jquery.dataTables.js",
"~/Scripts/javascripts/jquery.masonry.min.js",
"~/Plugins/plugins/camera/scripts/camera.min.js"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(

                      "~/Content/stylesheets/foundation.min.css",
                      "~/Content/stylesheets/app.css",
                      "~/Content/stylesheets/style1.css",
                       "~/Content/stylesheets/color7.css",
                       "~/Content/stylesheets/Pagination.css",
                       "~/Content/stylesheets/fullcalendar.css",
                       "~/Content/stylesheets/jquery.dataTables.css",
                       "~/Content/stylesheets/lightbox.css",
                       "~/Content/Uploadify/uploadify.css",
                       "~/Plugins/plugins/titan/css/jquery.titanlighbox.css",
                       "~/Plugins/plugins/camera/css/camera.css"));

        }
    }
}
