using System.Collections.Generic;
using System.Text;

namespace LongHu.Web.Utility
{
    public class PagerBuilder
    {
        private class PagerLink
        {
            public string Title { get; set; }
            public int PageNo { get; set; }
            public string Class { get; set; }
            public bool Enabled { get; set; }
        }

        private readonly string _urlTemplate;
        private readonly List<PagerLink> _pagerLinks = new List<PagerLink>();

        public PagerBuilder(string urlTemplate)
        {
            _urlTemplate = urlTemplate;
        }

        public string PagerClass { get; set; }

        public void AddPage(string title, int pageNo)
        {
            AddPage(title, pageNo, string.Empty, true);
        }

        public void AddPage(string title,
            int pageNo,
            string itemClass,
            bool enabled)
        {
            var link = new PagerLink
            {
                PageNo = pageNo,
                Title = title,
                Class = itemClass,
                Enabled = enabled
            };
            _pagerLinks.Add(link);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("<div");

            if (!string.IsNullOrEmpty(PagerClass))
            {
                builder.Append(" class=\"");
                builder.Append(PagerClass);
                builder.Append("\"");
            }
            builder.Append(">");

            foreach (var link in _pagerLinks)
            {
                builder.Append("<a href=\"");
                if (link.Enabled)
                {
                    builder.AppendFormat(_urlTemplate, link.PageNo);
                }
                else
                {
                    builder.Append("javascript:void(0)");
                }
                builder.Append("\"");

                if (!string.IsNullOrEmpty(link.Class))
                {
                    builder.Append(" class=\"");
                    builder.Append(link.Class);
                    builder.Append("\"");
                }

                builder.Append(">");
                builder.Append(link.Title);
                builder.Append("</a>");
            }

            builder.Append("</div>");
            return builder.ToString();
        }
    }
}
