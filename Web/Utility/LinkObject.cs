namespace LongHu.Web.Utility
{
    /// <summary>
    /// Link Object class 
    /// </summary>
    public class LinkObject
    {
        private string _linkText = "link";

        /// <summary>
        /// get or set Link Text 
        /// </summary>
        public string LinkText
        {
            get { return _linkText; }
            set { _linkText = value; }
        }

        /// <summary>
        /// get or set Link Action
        /// </summary>
        public string LinkAction { get; set; }

        /// <summary>
        /// get or set link Style
        /// </summary>
        public string LinkStyle { get; set; }

        /// <summary>
        /// get or set Parameter one
        /// </summary>
        public string Paramater1 { get; set; }

        /// <summary>
        /// get or set Parameter two
        /// </summary>
        public string Paramater2 { get; set; }

        /// <summary>
        /// get or set Parameter three
        /// </summary>
        public string Paramater3 { get; set; }

        /// <summary>
        /// get or set Del Alert Message
        /// </summary>
        public object DelAlertMessage { get; set; }

        private string _functionName = "alert('请自添加删除事件')";
        /// <summary>
        /// get or set Function Name
        /// </summary>
        public string FunctionName
        {
            get { return _functionName; }
            set { _functionName = value; }
        }
        private string _deleteMessage = "你确定要删除选中项的数据吗？";
        /// <summary>
        /// get or set Delete Message
        /// </summary>
        public string DeleteMessage
        {
            get { return _deleteMessage; }
            set { _deleteMessage = value; }
        }

        /// <summary>
        /// get or set Search Name Array
        /// </summary>
        public string[] SearchName { get; set; }

        /// <summary>
        /// get or set Search Value Array
        /// </summary>
        public string[] SearchValue { get; set; }

        /// <summary>
        /// LinkObject Constructor
        /// </summary>
        /// <param name="linkText">link Text</param>
        /// <param name="linkAction">link Action</param>
        public LinkObject(string linkText, string linkAction)
        {
            _linkText = linkText;
            LinkAction = linkAction;
        }
        /// <summary>
        /// LinkObject Constructor
        /// </summary>
        /// <param name="linkText">link Text</param>
        /// <param name="linkAction">link Action</param>
        /// <param name="linkSytle"></param>
        public LinkObject(string linkText, string linkAction, string linkSytle)
        {
            _linkText = linkText;
            LinkAction = linkAction;
            LinkStyle = linkSytle;
        }
        /// <summary>
        /// LinkObject Constructor
        /// </summary>
        /// <param name="linkText">link Text</param>
        /// <param name="linkAction">link Action</param>
        /// <param name="linkSytle">link Sytle</param>
        /// <param name="p1">parameter</param>
        public LinkObject(string linkText, string linkAction, string linkSytle, string p1)
        {
            _linkText = linkText;
            LinkAction = linkAction;
            LinkStyle = linkSytle;
            Paramater1 = p1;
        }
        /// <summary>
        /// LinkObject Constructor
        /// </summary>
        /// <param name="linkText">link Text</param>
        /// <param name="linkAction">link Action</param>
        /// <param name="linkSytle">link Sytle</param>
        /// <param name="p1">parameter</param>
        /// <param name="p2">parameter</param>
        public LinkObject(string linkText, string linkAction, string linkSytle, string p1, string p2)
        {
            _linkText = linkText;
            LinkAction = linkAction;
            LinkStyle = linkSytle;
            Paramater1 = p1;
            Paramater2 = p2;
        }
        /// <summary>
        /// LinkObject Constructor
        /// </summary>
        /// <param name="linkText">link Text</param>
        /// <param name="linkAction">link Action</param>
        /// <param name="linkSytle">link Sytle</param>
        /// <param name="p1">parameter</param>
        /// <param name="p2">parameter</param>
        /// <param name="p3">parameter</param>
        public LinkObject(string linkText, string linkAction, string linkSytle, string p1, string p2, string p3)
        {
            _linkText = linkText;
            LinkAction = linkAction;
            LinkStyle = linkSytle;
            Paramater1 = p1;
            Paramater2 = p2;
            Paramater3 = p3;
        }


        /// <summary>
        /// LinkObject Constructor for delete
        /// </summary>
        /// <param name="linkText">link Text</param>
        /// <param name="linkAction">link Action</param>
        /// <param name="linkSytle">link Sytle</param>
        /// <param name="delAlertMessage">Del Alert Message（注意此处必须为【Del】）</param>
        /// <param name="functionName">Function name </param>
        /// <param name="deleteMessage">Remove ask for information</param>
        public LinkObject(string linkText, string linkAction, string linkSytle, object delAlertMessage, string functionName, string deleteMessage)
        {
            _linkText = linkText;
            LinkAction = linkAction;
            LinkStyle = linkSytle;
            DelAlertMessage = delAlertMessage;
            if (functionName != null)
            {
                FunctionName = functionName.Trim().Length > 0 ? functionName : _functionName;
            }
            if (deleteMessage != null)
            {
                DeleteMessage = deleteMessage.Trim().Length > 0 ? deleteMessage : _deleteMessage;
            }
        }


        /// <summary>
        /// Custom hyperlink (ID value has been passed passed by default)
        /// </summary>
        /// <param name="linkText">link Text</param>
        /// <param name="linkAction">link Action</param>
        /// <param name="linkSytle">link Sytle</param>
        /// <param name="strSearchName">Search Name</param>
        /// <param name="strSearchValue">Search Value</param>
        public LinkObject(string linkText, string linkAction, string linkSytle, string[] strSearchName, string[] strSearchValue)
        {
            _linkText = linkText;
            LinkAction = linkAction;
            LinkStyle = linkSytle;
            SearchName = strSearchName;
            SearchValue = strSearchValue;
        }
    }
}
