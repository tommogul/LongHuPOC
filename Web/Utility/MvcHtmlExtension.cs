using System.Web;
using System;
using System.Web.Mvc;
using System.Text;
namespace LongHu.Web.Utility
{
    /// <summary>
    /// Mvc Html Extensions for Globalization/Localization
    /// </summary>
    public static class MvcHtmlExtension
    {
        /// <summary>
        /// SimplePager extension for HtmlHelper
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageCount"></param>
        /// <param name="urlTemplate"></param>
        /// <param name="pagerClass"></param>
        /// <returns></returns>
        public static IHtmlString SimplePager(this HtmlHelper helper,
            int currentPage, int pageCount, string urlTemplate,
            string pagerClass)
        {
            if (currentPage < 0) currentPage = 1;
            if (pageCount < 0) pageCount = 0;

            var pager = new PagerBuilder(urlTemplate) {PagerClass = pagerClass};

            if (currentPage > 1)
            {
                pager.AddPage("&lt;&lt;", 1, "enabled", true);
                pager.AddPage("&lt;", 1, "enabled", true);
            }
            else
            {
                pager.AddPage("&lt;&lt;", 1, "disabled", false);
                pager.AddPage("&lt;", 1, "disabled", false);
            }

            var start = Math.Max(currentPage - 2, 1);
            var end = Math.Min(pageCount, currentPage + 2);

            for (var i = start; i <= end; i++)
            {
                pager.AddPage(i.ToString(), i, i == currentPage ? "current" : "enabled", true);
            }

            if (currentPage < pageCount)
            {
                pager.AddPage("&gt;", currentPage + 1, "enabled", true);
                pager.AddPage("&gt;&gt;", pageCount, "enabled", true);
            }
            else
            {
                pager.AddPage("&gt;", currentPage + 1, "disabled", false);
                pager.AddPage("&gt;&gt;", pageCount, "disabled", false);
            }
            return new HtmlString(pager.ToString());
        }
        /// <summary>
        /// Genernate DropDownList
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="SelectListName"></param>
        /// <param name="SelectItems">DataSource SelectItems's Item must has Id Name property</param>
        /// <param name="SelectedValue">Selected Item</param>
        /// <param name="Attributes"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownList(this HtmlHelper helper, string SelectListName, SelectList SelectItems, string SelectedValue, string Attributes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<select");
            if (SelectListName.Trim() != "")
            {
                sb.Append(" name=\"" + SelectListName + "\"");
            }
            else
            {
                return new MvcHtmlString(string.Empty);
            }

            if (!string.IsNullOrEmpty(Attributes) && Attributes.Trim() != "")
            {
                sb.Append(" " + Attributes.Trim());
            }

            sb.Append(">");

            string itemValue = string.Empty;
            string itemText = string.Empty;
            foreach (object item in SelectItems.Items)
            {
                //itemValue = item.GetType().GetProperty("Id").GetValue(item,null).ToString();
                //itemText = item.GetType().GetProperty("Name").GetValue(item, null).ToString(); 
                itemValue = item.GetType().GetProperty(SelectItems.DataValueField).GetValue(item, null).ToString();
                itemText = item.GetType().GetProperty(SelectItems.DataTextField).GetValue(item, null).ToString();
                if (itemValue == SelectedValue)
                {
                    sb.Append("<option value=\"" + itemValue + "\" selected=\"selected\">" + itemText + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + itemValue + "\">" + itemText + "</option>");
                }
            }

            sb.Append("</select>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}
