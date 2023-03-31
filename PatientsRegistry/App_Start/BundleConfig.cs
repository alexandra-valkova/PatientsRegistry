using System.Web.Optimization;

namespace PatientsRegistry
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                        "~/scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/scripts/modernizr-{version}.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
