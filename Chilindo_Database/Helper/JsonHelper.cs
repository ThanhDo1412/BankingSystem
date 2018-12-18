using Newtonsoft.Json;

namespace Chilindo_Data.Helper
{
    public static class JsonHelper
    {
        public static string JsonSerialize(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            //var settings = new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore,
            //    DefaultValueHandling = DefaultValueHandling.Ignore
            //};

            return JsonConvert.SerializeObject(obj);
        }

        public static T JsonDeserialize<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}