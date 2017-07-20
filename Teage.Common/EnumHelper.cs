using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Common
{
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举类型值的描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionFromEnumValue(System.Enum value)
        {
            try
            {
                DescriptionAttribute attribute = value.GetType()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;
                return attribute == null ? value.ToString() : attribute.Description;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将枚举转换为Hashtable;T为枚举名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Hashtable EnumToHashtable<T>()
        {
            Hashtable has = new Hashtable();
            foreach (int i in Enum.GetValues(typeof(T)))
            {
                has.Add(i.ToString(), Enum.GetName(typeof(T), i));
            }
            return has;
        }

        /// <summary>
        /// 将枚举转换为泛型类;T为枚举名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KVP<int, string>> EnumToList<T>()
        {
            List<KVP<int, string>> list = new List<KVP<int, string>>();
            foreach (int i in Enum.GetValues(typeof(T)))
            {
                KVP<int, string> tmp = new KVP<int, string>(i, Enum.GetName(typeof(T), i));
                list.Add(tmp);
            }
            return list;
        }

        /// <summary>
        /// 存储枚举的键值对对象
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        public class KVP<K, V>
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public KVP(K k, V v)
            {
                this.Key = k;
                this.Value = v;
            }
        }
    }
}
