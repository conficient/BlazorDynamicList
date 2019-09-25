using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    /// <summary>
    /// Helper class to serialize/deserialise with typename handling
    /// </summary>
    public static class TypedSerializer
    {
        /// <summary>
        /// Serialize this list of data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        /// <summary>
        /// Deserialize a an object with TypeNameHandling 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

    }

}
