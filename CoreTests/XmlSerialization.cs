using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Basilisk.Tests.Core
{
    internal static class XmlSerialization
    {
        public static T Roundtrip<T>(T obj)
        {
            using (var mem = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(mem, obj);
                mem.Position = 0;
                using (var reader = XmlDictionaryReader.CreateTextReader(mem, new XmlDictionaryReaderQuotas()))
                {
                    return (T)serializer.ReadObject(reader);
                }
            }
        }

        public static string Serialize<T>(T obj)
        {
            using (var mem = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(mem, obj);
                mem.Position = 0;
                using (var reader = new StreamReader(mem))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
