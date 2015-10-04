using System;
using System.IO;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Basilisk.Tests.Core
{
    internal static class JsonSerialization
    {
        private static JsonSerializerSettings Settings =>
            new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects
            };

        public static ComponentT Roundtrip<ComponentT>(ComponentT component)
        {
            var json = JsonConvert.SerializeObject(component, Settings);
            return JsonConvert.DeserializeObject<ComponentT>(json, Settings);
        }

        public static string Serialize<ComponentT>(ComponentT component)
        {
            return JsonConvert.SerializeObject(component, Settings);
        }

        public static ComponentT Deserialize<ComponentT>(string json)
        {
            return JsonConvert.DeserializeObject<ComponentT>(json, Settings);
        }
    }
}
