namespace BasicLibraries.CommonControl.Json
{
    using Newtonsoft.Json;
    public static class JsonnExtensionMethods
    {
        /// <summary>
        /// object类型转换为JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToJson(this object obj)
        {
            if (obj != null)
            {
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }

            return default;
        }


        public static T JsonToObj<T>(this string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch
            {
                return default;
            }
        }
    }
}
