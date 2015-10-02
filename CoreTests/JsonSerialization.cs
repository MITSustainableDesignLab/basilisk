using System;
using System.IO;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace CoreTests
{
    internal static class JsonSerialization
    {
        public static ComponentT Roundtrip<ComponentT>(ComponentT component)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var json = JsonConvert.SerializeObject(component, Formatting.Indented, settings);
            return JsonConvert.DeserializeObject<ComponentT>(json);
        }

        public static string Serialize<ComponentT>(ComponentT component)
        {
            return JsonConvert.SerializeObject(component, Formatting.Indented);
        }

        public static ComponentT Deserialize<ComponentT>(string json)
        {
            return JsonConvert.DeserializeObject<ComponentT>(json);
        }
    }
}
