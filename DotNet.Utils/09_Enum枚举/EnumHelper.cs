/// <summary>
/// 使用前提： 
/// 类 说 明： 根据枚举值获取自定义的值
/// 编 码 人： 张贺
/// 创建日期： 2015-03-11
/// 更新日期： 
/// 更新内容:
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Utils
{
    public class EnumHelper
    {

        /// <summary>
        /// 获取枚举值,推荐用此方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(T value) where T : struct
        {
            int num = Convert.ToInt32(value);
            if (num < 0x41)
            {
                return num;
            }
            return num;
        }


        /// <summary>
        /// 根据枚举获取自定义值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes =
                (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        } 
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDescriptionAttribute : Attribute
    {
        private string description;
        public string Description { get { return description; } }

        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }
}
