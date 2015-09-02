using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public abstract class LibraryComponent
    {
        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public string DataSource { get; set; }

        [DataMember]
        public string Name { get; set; }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext context)
        {
            foreach (var prop in GetType().GetProperties())
            {
                var att = prop.GetCustomAttribute<DefaultValueAttribute>();
                if (att == null) { continue; }
                prop.SetValue(this, att.Value);
            }
        }
    }
}
