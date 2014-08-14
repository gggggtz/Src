/// <copyright>
/// Copyright ©  2014 Microsoft Corporation. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.Utility
{
    public class AssemblyHelper
    {
        public static string GetAssemblyEmbeddedResourceString(Type typeInAssembly, string resourceName)
        {
            Stream s = GetAssemblyEmbeddedResourceStream(typeInAssembly, resourceName);
            StreamReader sr = new StreamReader(s);
            string result = sr.ReadToEnd();
            sr.Close();
            s.Close();
            return result;
        }

        public static Stream GetAssemblyEmbeddedResourceStream(Type typeInAssembly, string resourceName)
        {
            Assembly a = Assembly.GetAssembly(typeInAssembly);
            return a.GetManifestResourceStream(resourceName);
        }

        public static T DeserializeAssemblyEmbeddedResource<T>(Type typeInAssembly, string resourceName) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            Stream s = GetAssemblyEmbeddedResourceStream(typeInAssembly, resourceName);
            T result = serializer.Deserialize(s) as T;
            s.Close();
            return result;
        }
    }
}
