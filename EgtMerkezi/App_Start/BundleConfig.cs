using System.Web;
using System.Web.Optimization;

namespace EgtMerkezi
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                       "~/Scripts/plugins.js",
                      "~/Scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/jquery-migrate-1.2.1.min.js",
                        "~/Scripts/modernizr.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/style.css"));

            //* Admin *//

            bundles.Add(new StyleBundle("~/assets/css/styles.css").Include(
                      "~/assets/css/styles.css",
                      "~/assets/demo/variations/header-blue.css"));

            bundles.Add(new StyleBundle("~/assets/css/ie8.css").Include(
                      "~/assets/css/ie8.css"));

            bundles.Add(new ScriptBundle("~/assets/js/plugin-before").Include(
                      "~/assets/js/jqueryui-1.10.3.min.js",
                      "~/assets/js/bootstrap.min.js",
                      "~/assets/js/enquire.js",
                      "~/assets/js/jquery.cookie.js",
                      "~/assets/js/jquery.nicescroll.min.js"));

            bundles.Add(new ScriptBundle("~/assets/js/plugin-after").Include(
                      "~/assets/js/placeholdr.js",
                      "~/assets/js/application.js"));

            bundles.Add(new ScriptBundle("~/Template/js/master_core.js").Include(
                      "~/Template/js/master_core.js"));

            bundles.Add(new ScriptBundle("~/Template/js/showLoading.js").Include(
                     "~/Template/js/showLoading.js"));

            bundles.Add(new StyleBundle("~/Template/css/master_styles.css").Include(
                      "~/Template/css/master_styles.css"));

        }
    }
}
