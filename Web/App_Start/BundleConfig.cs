using System.Web;
using System.Web.Optimization;

namespace LongHu.Web
{
     public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
             bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js"
                        , "~/Scripts/jquery-ui-{version}.js"
                        , "~/Scripts/jquery.unobtrusive*"
                        , "~/Scripts/jquery.validate*",
                         "~/Scripts/jquery-ui-i18n.js",
                         "~/Scripts/PageJs/globalize.js",
                       "~/Scripts/PageJs/globalize.culture.es.js",
                       "~/Scripts/PageJs/globalize.culture.pt-BR.js",
                         "~/Scripts/global.js",
                         "~/Scripts//PageJs/system.js",
                         "~/Scripts/jquery.json-2.2.js"
                        ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/themes/SearchCondition.css",
                 "~/Content/themes/System.css",
                "~/Content/themes/arise/base/Arise2.sp1.css"
                ));
            bundles.Add(new StyleBundle("~/Content/themes/arise/jquery-ui/css").Include(

                        "~/Content/themes/arise/jquery-ui/jquery-ui.base.css",
                        "~/Content/themes/arise/jquery-ui/jquery.ui.core.css",
                        "~/Content/themes/arise/jquery-ui/jquery.ui.selectable.css",
                        "~/Content/themes/arise/jquery-ui/jquery.ui.autocomplete.css",
                        "~/Content/themes/arise/jquery-ui/jquery.ui.button.css",
                        "~/Content/themes/arise/jquery-ui/jquery.ui.dialog.css",
                        "~/Content/themes/arise/jquery-ui/jquery.ui.datepicker.css",
                        "~/Content/themes/arise/jquery-ui/jquery.ui.theme.css"
                        ));
            bundles.Add(new StyleBundle("~/Scripts/PageJs/UserRoleIndex").Include("~/Scripts/PageJs/UserRoleIndex.js"));
        }
    }
}
