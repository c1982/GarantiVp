/**
MIT License

Copyright (c) 2014 Oğuzhan YILMAZ and contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace GarantiVP
{
    public static class GVPSHelpers
    {
        public static string Get(this IDictionary<string, string[]> dic, string key)
        {
            string ret = null;
            if (dic == null)
                throw new ArgumentNullException("dic");
            if (dic.ContainsKey(key))
                ret = dic[key].FirstOrDefault();
            return ret;
        }
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

        public static T GetValueFromXmlEnumName<T>(this string val) where T : struct
        {
            T ret = default(T);
            Type type = typeof(T);
            foreach (T e in Enum.GetValues(type))
            {
                var XmlName = (e as Enum).GetXmlEnumName();
                if(string.Equals(val, XmlName))
                {
                    ret = e;
                    break;
                }
            }
            return ret; 
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
