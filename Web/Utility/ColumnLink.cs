namespace LongHu.Web.Utility
{
    /// <summary>
    /// Column Link object class 
    /// </summary>
    public class ColumnLink
    {
        /// <summary>
        /// get or set As the first few links
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// get or set Link Object
        /// </summary>
        public LinkObject Link { get; set; }

        /// <summary>
        /// Column Link class Constructor
        /// </summary>
        /// <param name="column">column number(From 0)</param>
        /// <param name="link">LinkObject</param>
        public ColumnLink(int column, LinkObject link)
        {
            Column = column;
            Link = link;
        }
    }
}

