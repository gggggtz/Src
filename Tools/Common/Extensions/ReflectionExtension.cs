/// <copyright>
/// Copyright ©  2014 Microsoft Corporation. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Extensions
{
	public static class ReflectionExtension
	{
		public static T GetCustomAttribute<T>(this Type type) where T : Attribute
		{
			return GetCustomAttribute<T>(type, false);
		}

		public static T GetCustomAttribute<T>(this Type type, bool inherit) where T : Attribute
		{
			object[] attrs = type.GetCustomAttributes(typeof(T), inherit);
			if (attrs != null && attrs.Length > 0)
			{
				return attrs[0] as T;
			}
			return null;
		}

        //public static T GetCustomAttribute<T>(this MemberInfo member) where T : Attribute
        //{
        //    return GetCustomAttribute<T>(member, false);
        //}

        //public static T GetCustomAttribute<T>(this MemberInfo member, bool inherit) where T : Attribute
        //{
        //    object[] attrs = member.GetCustomAttributes(typeof(T), inherit);
        //    if (attrs != null && attrs.Length > 0)
        //    {
        //        return attrs[0] as T;
        //    }
        //    return null;
        //}

	}
}
