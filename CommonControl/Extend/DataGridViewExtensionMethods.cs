namespace BasicLibraries.CommonControl.DataGridViewExtension
{
    using System;
    using System.Reflection;
    public static class DataGridViewExtensionMethods
    {
        /// <summary>
        /// 双缓存
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="setting"></param>
        public static void DoubleBuffered(this System.Windows.Forms.DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                 BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
