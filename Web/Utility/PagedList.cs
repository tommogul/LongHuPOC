using System.Collections.Generic;

namespace LongHu.Web.Utility
{
    /// <summary>
    /// Paged List class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedList
    {
        /// <summary>
        /// Paged List
        /// </summary>
        /// <param name="items">IEnumerable<T></param>
        /// <param name="pageIndex">page Index</param>
        /// <param name="pageSize">page Size</param>
        /// <param name="totalItemCount">total Item Count</param>
        /// <param name="identityColumnName">identity Column Name</param>
        /// <param name="sortExpression">sort Expression</param>
        /// <param name="str">Form Condition</param>
        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount, string identityColumnName, string sortExpression, string str)
        {
            PageSize = pageSize;
            AddRange(items);
            PageIndex = pageIndex;
            SortExpression = sortExpression;
            TotalItemCount = totalItemCount;
            totalPageCount = (totalItemCount + pageSize - 1) / pageSize;
            IdentityColumnName = identityColumnName;
            FormCondition = str;
        }

        /// <summary>
        /// Paged List
        /// </summary>
        /// <param name="items">IEnumerable<T></param>
        /// <param name="pageIndex">page Index</param>
        /// <param name="pageSize">page Size</param>
        /// <param name="totalItemCount">total Item Count</param>
        /// <param name="identityColumnName">identity Column Name</param>
        /// <param name="sortExpression">sort Expression</param>
        /// <param name="asc">Asc</param>
        /// <param name="str">Form Condition</param>
        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount, string identityColumnName, string sortExpression, string asc, string str)
        {
            PageSize = pageSize;
            AddRange(items);
            PageIndex = pageIndex;
            SortExpression = sortExpression;
            TotalItemCount = totalItemCount;
            totalPageCount = (totalItemCount + pageSize - 1) / pageSize;
            IdentityColumnName = identityColumnName;
            Asc = asc;
            FormCondition = str;
        }


        #region IPagedList Members

        /// <summary>
        /// get or set  Page Index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// get or set Page Size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// get or set Sort Expression
        /// </summary>
        public string SortExpression { get; set; }

        /// <summary>
        /// get or set Total Item Count
        /// </summary>
        public int TotalItemCount { get; set; }

        private int totalPageCount;
        /// <summary>
        /// get or set Total Page Count
        /// </summary>
        public int TotalPageCount
        {
            get
            {
                return totalPageCount;
            }
        }

        /// <summary>
        ///get or set  Identity Column Name
        /// </summary>
        public string IdentityColumnName { get; set; }

        /// <summary>
        /// get or set Asc
        /// </summary>
        public string Asc { get; set; }

        public string FormCondition { get; set; }

        #endregion
    }
}
