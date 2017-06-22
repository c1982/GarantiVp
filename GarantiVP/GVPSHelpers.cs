using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace GarantiVP
{
    public static class GVPSHelpers
    {
        public static XmlElement AddInput(this XmlElement xe, string name, string val, string type = "hidden")
        {
            var xeNew = xe.OwnerDocument.CreateElement("input");
            xeNew.SetAttribute("type", type);
            xeNew.SetAttribute("name", name);
            xeNew.SetAttribute("value", val);
            xe.AppendChild(xeNew);
            return xe;
        }

        public static string GetXmlEnumName(this Enum val) 
        {
            Type type = val.GetType();
            var finfo = type.GetField(Enum.GetName(type, val));
            return finfo.GetCustomAttributes(typeof(XmlEnumAttribute), false).Select(e => (XmlEnumAttribute)e).Where(e => e != null).Select(e => e.Name).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="val"></param>
        /// <remarks>https://stackoverflow.com/questions/3047125/retrieve-enum-value-based-on-xmlenumattribute-name-value</remarks>
        /// <returns></returns>
        public static TAttribute[] GetEnumAttributes<TEnum, TAttribute>(this TEnum val) where TAttribute : Attribute where TEnum : struct
        {
            Type type = val.GetType();
            var finfo = type.GetField(Enum.GetName(typeof(TEnum), val));
            TAttribute[] attList = finfo.GetCustomAttributes(typeof(TAttribute), false).Select(e => (TAttribute)e).Where(e => e != null).ToArray();
            return attList;
        }


        public static string GetPropertyName<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            return GetPropertyInfo(source, propertyLambda).Name;
        }

        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);
            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }
    }
}
