namespace LongHu.Web.Utility
{
    public interface IPagedList
    {
        /// <summary>
        /// Set or get Page Index
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// Set or get Page Size
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Set or get Sort Expression
        /// </summary>
        string SortExpression { get; set; }

        /// <summary>
        /// Set or get Total Item Count
        /// </summary>
        int TotalItemCount { get; set; }

        /// <summary>
        /// get Total Page Count
        /// </summary>
        int TotalPageCount { get; }

        /// <summary>
        /// Set or get Identity Column Name
        /// </summary>
        string IdentityColumnName { get; set; }

        /// <summary>
        /// Set or get Asc
        /// </summary>
        string Asc { get; set; }

        string FormCondition { get; set; }
    }
}

