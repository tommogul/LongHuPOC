using System;
using System.Collections.Generic;
using System.Linq;

namespace LongHu.Web.Utility
{
    /// <summary>
    /// Page linq Extensions class
    /// After the background check out all the entities (TOLIST) calls this method to display page
    /// </summary>
    public static class PagelinqExtensions
    {
        #region //First read all the data in the paging
        /// <summary>
        /// To Paged List
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="allitems">All items</param>
        /// <param name="pageIndex">page Index</param>
        /// <param name="pageSize">page Size</param>
        /// <param name="identityColumnName">identity Column Name</param>
        /// <returns>PagedList<T></returns>
        public static PagedList<T> ToPagedList<T>(this IList<T> allitems, int? pageIndex, int pageSize, string identityColumnName)
        {
            return ToPagedList(allitems, pageIndex, pageSize, identityColumnName, String.Empty);
        }

        /// <summary>
        /// To Paged List
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="allItems">All Items</param>
        /// <param name="pageIndex">page Index</param>
        /// <param name="pageSize">page Size</param>
        /// <param name="identityColumnName">identity Column Name</param>
        /// <param name="sort">sort</param>
        /// <returns>PagedList<T></returns>
        public static PagedList<T> ToPagedList<T>(this IList<T> allItems, int? pageIndex, int pageSize, string identityColumnName, string sort)
        {
            var truePageIndex = pageIndex ?? 0;
            var itemIndex = truePageIndex * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            var totalItemCount = allItems.Count();

            //Call pageList class
            return new PagedList<T>(pageOfItems, truePageIndex, pageSize, totalItemCount, identityColumnName, sort, null);
        }
        #endregion

        #region //After reading the page data directly
        /// <summary>
        /// To Paged List
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="pageOfItems">page Of Items</param>
        /// <param name="totalItemCount"> total Item Count</param>
        /// <param name="pageIndex">page Index</param>
        /// <param name="pageSize">page Size</param>
        /// <param name="identityColumnName">identity Column Name</param>
        /// <returns>PagedList<T></returns>
        public static PagedList<T> ToPagedList<T>(this IList<T> pageOfItems, int totalItemCount, int? pageIndex, int pageSize, string identityColumnName)
        {
            return ToPagedList(pageOfItems, totalItemCount, pageIndex, pageSize, identityColumnName, String.Empty, string.Empty, null);
        }
        /// <summary>
        /// To Paged List
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="pageOfItems">page Of Items</param>
        /// <param name="totalItemCount"> total Item Count</param>
        /// <param name="pageIndex">page Index</param>
        /// <param name="pageSize">page Size</param>
        /// <param name="identityColumnName">identity Column Name</param>
        /// <param name="sort">sort</param>
        /// <param name="asc">asc</param>
        /// <param name="str">Form Condition</param>
        /// <returns>PagedList<T></returns>
        public static PagedList<T> ToPagedList<T>(this IList<T> pageOfItems, int totalItemCount, int? pageIndex, int pageSize, string identityColumnName, string sort, string asc, string str)
        {
            var truePageIndex = pageIndex ?? 0;

            //Call pageList class
            return new PagedList<T>(pageOfItems, truePageIndex, pageSize, totalItemCount, identityColumnName, sort, asc, str);
        }
        #endregion
    }
}
