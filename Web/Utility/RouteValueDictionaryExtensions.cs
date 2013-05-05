using System.Web;
using System.Web.Routing;

namespace LongHu.Web.Utility
{
    public static class RouteValueDictionaryExtensions
    {
        /// <summary>
        /// 路由扩展
        /// 添加路由历史记录，确保路由字典键与Action参数一致
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static RouteValueDictionary AddQueryStringParameters(this RouteValueDictionary dict)
        {
            var querystring = HttpContext.Current.Request.QueryString;

            foreach (var key in querystring.AllKeys)
                if (!dict.ContainsKey(key))
                    dict.Add(key, querystring.GetValues(key)[0]);
            return dict;
        }

        /// <summary>
        /// 路由扩展
        /// 从路由字典中移除指定的路由参数
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="keysToRemove"></param>
        /// <returns></returns>
        public static RouteValueDictionary ExceptFor(this RouteValueDictionary dict, params string[] keysToRemove)
        {
            foreach (var key in keysToRemove)
                if (dict.ContainsKey(key))
                    dict.Remove(key);
            return dict;
        }

    }
}
