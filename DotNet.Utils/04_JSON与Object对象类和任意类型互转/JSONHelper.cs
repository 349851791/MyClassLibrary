/// <summary>
/// 使用前提:   添加NuGet包的引用:JSON.NET
/// 类 说 明：  对象和json的互转
/// 编 码 人：  张贺
/// 创建日期:   2015-03-10
/// 更新记录:   2015-10-22
///             1.将原来的命名空间DotNet.Utils.JSON修改为DotNet.Utils 
///             2.修改此类为静态.
/// </summary>

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DotNet.Utils
{
    /// <summary>
    /// JSON转换帮助类
    /// </summary>
    public static class JSONHelper
    {
        private static JsonSerializerSettings _jsonSettings;
        /// <summary>
        /// 默认转换时间格式为yyyy-MM-dd HH:mm:ss
        /// </summary> 
        static JSONHelper()
        {
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            _jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            _jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            _jsonSettings.Converters.Add(datetimeConverter);
        }


        /// <summary>
        /// 类(实体类、数组、DataTable等)转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            try
            {
                if (null == obj)
                {
                    return null;
                }
                else
                {
                    string temp = JsonConvert.SerializeObject(obj, Formatting.None, _jsonSettings);
                    return temp;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 类(实体类、数组、DataTable等)转json,属性为空值时,值返回字段名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson_OnlyColumn(object obj)
        {
            try
            {
                if (null == obj)
                {
                    return null;
                }
                else
                {

                    _jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
                    string temp = JsonConvert.SerializeObject(obj, Formatting.None, _jsonSettings).Replace("null","''");
                    return temp;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// json转类(实体类、数组、DataTable等)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return default(T);
            }
        }

        /// <summary>
        /// 临时数据转换json
        /// </summary>
        /// <param name="list">object类型的集合</param>
        /// <param name="jsonName">转换为json的名称,默认为myJson</param>
        /// <returns></returns>
        public static string LinqToJson(List<object> list,string jsonName="myJson")
        {
            JArray array = new JArray();
            foreach (var item in list)
            {
                array.Add(item);
            }
            JObject o = new JObject();
            o[jsonName] = array;
            return o.ToString();
        }
       

        /// <summary>
        /// 根据JSON的Key获取它的值
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="key">复制json可以通过"."获取值,例:List.0.Names</param>
        /// <returns></returns>
        public static string GetValueByKey(string json, string key)
        {
            try
            {
                if (null == key)
                {
                    return null;
                }
                else
                {
                    JObject jb = JObject.Parse(json);
                    string[] strArray = key.Split('.');
                    Newtonsoft.Json.Linq.JToken jbTemp = jb;
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        string str = strArray[i];
                        if (i % 2 == 0)
                        {
                            jbTemp = jbTemp[str];
                        }
                        else
                        {
                            jbTemp = jbTemp[Convert.ToInt32(str)];
                        }
                    }

                    string temp = jbTemp.ToString();;
                    return temp;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "";
            }
        }

    }
}
