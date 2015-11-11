using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls
{
    internal static class Util
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TKey : class
        {
            if (key == null) { return default(TValue); }
            var res = default(TValue);
            dict.TryGetValue(key, out res);
            return res;
        }
    }
}
