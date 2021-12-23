namespace VNAControl.VNA
{
    using BasicLibraries.CommonControl.Json;
    using BasicLibraries.VNAControl.Format;
    using System;
    using System.IO;
    using VNAControl.Forms;

    /// <summary>
    /// VNA公共基础类
    /// </summary>
    public class VNACommonData
    {
        /// <summary>
        /// VNA校准文件路径
        /// </summary>
        public const string VNACalFilePath = ".\\vnaconfig\\vna_cal_config.json";

        /// <summary>
        /// 显示校准配置界面
        /// </summary>
        public static void ShowVNAMainForm()
        {
            FrmVNAConfigMain.Instance.ShowDialog();
        }

        /// <summary>
        /// 获取VNA配置的接口
        /// </summary>
        /// <param name="allParameters">返回的数据</param>
        /// <param name="path">文件路径</param>
        /// <returns>返回一个List,包含所有配置,有多少台就会有多少个count</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="FileLoadException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static bool GetVNAAllParameters(out VNAAllParameter[] allParameters, string path = VNACalFilePath)
        {
            if (Path.GetExtension(path).ToLower() != ".json")
            {
                throw new ArgumentException("文件类型错误,仅限json文件");
            }
            try
            {
                allParameters = File.ReadAllText(path).JsonToObj<VNAAllParameter[]>();
                return true;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (FileLoadException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
