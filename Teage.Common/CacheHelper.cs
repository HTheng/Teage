using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Teage.Common
{
    public class CacheHelper
    {
        /// <summary>
        /// 获取缓存中的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            Cache cache = HttpRuntime.Cache;
            return cache[key];
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            Cache cache = HttpRuntime.Cache;
            cache[key] = cache;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void Remove(string key)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Remove(key);
        }
    }
}
