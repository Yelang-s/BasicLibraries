namespace BasicLibraries.CommonControl
{
    using Microsoft.Win32;

    /// <summary>
    /// 判断windows电脑中有无安装指定程序类
    /// </summary>
    public class CheckAppInPC
    {
        /// <summary>
        /// 用于判断windows电脑中有无安装指定程序
        /// </summary>
        /// <param name="softname">软件名称</param>
        /// <returns></returns>
        public static bool IsTheAppInPC(string softname)
        {
            softname += ".exe";
            if (SoftIsInLocalMachine(softname))
            {
                return true;
            }
            else if (SoftIsInUsers(softname))
            {
                return true;
            }
            return false;
        }

        private static bool SoftIsInLocalMachine(string softname)
        {
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\", false);
            foreach (string keyName in regSubKey.GetSubKeyNames())
            {
                if (keyName.Contains(softname))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool SoftIsInUsers(string softname)
        {
            RegistryKey regKey = Registry.Users;
            foreach (string keyName in regKey.GetSubKeyNames())
            {
                if (keyName.ToLower().Contains("classes"))
                {
                    string subKeyName = keyName.Substring(0, keyName.Length - 8);
                    RegistryKey regSubKey = regKey.OpenSubKey(subKeyName + @"\Software\Microsoft\Windows\CurrentVersion\App Paths\", false);
                    foreach (string subName in regSubKey.GetSubKeyNames())
                    {
                        if (subName.Contains(softname))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
