namespace BasicLibraries.CommonControl
{
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    class CAPI
    {
        //GetPrivateProfileString是一个计算机函数，功能是为初始化文件中指定的条目取得字串，是编辑语言中的一种函数结构
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        public static extern int GetPrivateProfileString(string lpSectionName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        //为初始化文件中指定的条目获取它的整数值
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileInt")]
        public static extern int GetPrivateProfileInt(string lpSectionName, string lpKeyName, int lpDefault, string lpFileName);

        //初始化文件指定小节设置一个字串
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        public static extern int WritePrivateProfileString(string lpSectionName, string lpKeyName, string lpString, string lpFileName);
    }

    public class ModINI<T>
    {
        public static string strFileName = Application.StartupPath + @"\Parameter.ini";

        //从INI文件中读出字符串格式的值
        private static int GetPrivateProfileString(string lpSectionName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString)
        {
            return CAPI.GetPrivateProfileString(lpSectionName, lpKeyName, lpDefault, lpReturnedString, 255, strFileName);
        }
        //从INI文件中读出整型格式的值
        private static int GetPrivateProfileInt(string lpSectionName, string lpKeyName, int lpDefault)
        {
            return CAPI.GetPrivateProfileInt(lpSectionName, lpKeyName, lpDefault, strFileName);
        }
        //从向INI文件中写入字符串格式的值
        private static int WritePrivateProfileString(string lpSectionName, string lpKeyName, string lpString)
        {
            return CAPI.WritePrivateProfileString(lpSectionName, lpKeyName, lpString, strFileName);
        }
        public static string ReadINI(string keyName, string sectionName = "System")
        {
            if (!File.Exists(strFileName))
            {
                MessageBox.Show("INI File lost!");
                return "";
            }
            try
            {
                StringBuilder strValue1 = new StringBuilder(50);
                string strValue;
                GetPrivateProfileString(sectionName, keyName, "0", strValue1);
                strValue = strValue1.ToString();
                return strValue;
            }
            catch
            {
                return "";
            }
        }
        public static bool WriteINI(string keyName, string value, string sectionName = "System")
        {
            if (!File.Exists(strFileName))
            {
                MessageBox.Show("INI File lost!");
                return false;
            }
            try
            {
                WritePrivateProfileString(sectionName, keyName, value);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool ReadINI(ref T para1, string sectionName = "System")
        {
            if (!File.Exists(strFileName))
            {
                MessageBox.Show("INI File lost!");
                return false;
            }

            try
            {
                foreach (FieldInfo fieldInfo in para1.GetType().GetFields())
                {
                    StringBuilder strValue1 = new StringBuilder(50);
                    string strValue;
                    GetPrivateProfileString(sectionName, fieldInfo.Name, "0", strValue1);
                    strValue = strValue1.ToString();
                    object objN = fieldInfo.GetValue(para1);
                    if (fieldInfo.GetValue(para1) is int)
                        fieldInfo.SetValue(para1, System.Convert.ToInt32(strValue));
                    else if (fieldInfo.GetValue(para1) is double)
                        fieldInfo.SetValue(para1, System.Convert.ToDouble(strValue));
                    else if (fieldInfo.GetValue(para1) is string)
                        fieldInfo.SetValue(para1, strValue);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool WriteINI(T para1, string sectionName = "System")
        {
            if (!File.Exists(strFileName))
            {
                MessageBox.Show("INI File lost!");
                return false;
            }
            try
            {
                foreach (FieldInfo fieldInfo in para1.GetType().GetFields())
                    WritePrivateProfileString(sectionName, fieldInfo.Name, fieldInfo.GetValue(para1).ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

