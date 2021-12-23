namespace BasicLibraries.CommonControl
{
    using System.IO;
    using System.Reflection;
    public class AssemblyInfos
    {
        /// <summary>
        /// 获取程序集标题
        /// </summary>
        /// <returns></returns>
        public string GetAssemblyTitle()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute title = (AssemblyTitleAttribute)attributes[0];
                if (!string.IsNullOrEmpty(title.Title))
                    return title.Title;
            }
            return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }

        /// <summary>
        /// 获取程序集版本
        /// </summary>
        /// <returns></returns>
        public string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// 获取程序集描述说明
        /// </summary>
        /// <returns></returns>
        public string GetAssemblyDescription()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }



    }
}
