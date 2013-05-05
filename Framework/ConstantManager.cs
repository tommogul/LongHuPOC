using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LongHu.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;
    using System.Web;
    public class ConstantManager
    {
        #region page
        public static int PageSize
        {
            get
            {
                var result = GetAppSetting("PageSize");
                return result == string.Empty ? 15 : int.Parse(result.Trim());
            }
        }
        public static string GetAppSetting(string keyWord)
        {
            var result = string.Empty;
            if (ConfigurationManager.AppSettings[keyWord] != null)
                result = ConfigurationManager.AppSettings[keyWord].Trim();
            return result;
        }
		public static int CacheCurrentUserExpirationTime
        {
            get
            {
                var result = GetAppSetting("CacheCurrentUserExpirationTime");
                return result == string.Empty ? 30 : int.Parse(result.Trim());
            }
        }
        public static TimeSpan CacheCurrentUserSlidingExpiration
        {
            get
            {
                return new TimeSpan(0, CacheCurrentUserExpirationTime, 0);
            }
        }
        #endregion
		public static string  TextBoxMandatoryShortClass
        {
            get { return "textBox mandatory short"; }
        }
        public static string DropDownListMandatoryShortClass
        {
            get { return "dropDownList mandatory short"; }
        }
        public static string SessionUserKey
        {
            get { return "User"; }
        }
		public static string CurrentUserIdKey
        {
            get
            {
                return "CurrentUserIdKey";
            }
        }
		public static string DisableDuplicateSubmitForm { get { return "DisableDuplicateSubmitForm"; } }
		public static Decimal GetCurrentUserId()
        {
            if (
                   null != HttpContext.Current &&
                   HttpContext.Current.Items.Contains(ConstantManager.CurrentUserIdKey))
            {
                return Decimal.Parse(HttpContext.Current.Items[ConstantManager.CurrentUserIdKey].ToString());
            }
            else
            {
                return -1;
            }

        }
    }
}


