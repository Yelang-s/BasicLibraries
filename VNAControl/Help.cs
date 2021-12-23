namespace VNAControl
{
    internal class HelpC
    {
        public static string GetCalFileHelp()
        {

            string help = "校准文件配置帮助\r\n" +
                "1. Port:按实际选择\r\n" +
                "2. Channel:按实际选择,请勿重复选择\r\n" +
                "3. ChannelName:请勿包含特殊字符,仅支持字母数字,以字母开头\r\n" +
                "4. StartFrequency:按实际需求填写,仅支持数字\r\n" +
                "5. StopFrequency:按实际需求填写,仅支持数字\r\n" +
                "6. Point:按实际需求填写,仅支持数字\r\n" +
                "7. SmoothingEnable:按实际选择是否开启,开启后会使波形更平滑\r\n" +
                "8. SmoothingAperture:平滑度[Kyesight E5071C设置范围为0.05到25]\r\n" +
                "9. TraceNo:默认全部为1\r\n" +
                "10. TraceName:请勿包含特殊字符,仅支持字母数字,以字母开头\r\n" +
                "11. SParameter:按实际选择,目前仅支持[S11,S22,S33,S44],其余不支持,请勿选择\r\n" +
                "12. ReadType:按需求选择\r\n" +
                "13. ShowType:按需求选择\r\n" +
                "14. SwitchEnabel:按需求选择\r\n" +
                "15. SwitchChannel:按实际填写,仅为数字\r\n" +
                "\r\n" +
                "关于ReadType&ShowType参数注释\r\n" +
                "Note:罗德施瓦茨仅支持部分参数,请查阅相关资料\r\n" +
                "   ///<summary>\r\n" +
                "   /// 指定对数幅度格式\r\n" +
                "   /// 单位 dB\r\n" +
                "   /// 应用实例：\r\n" +
                "   /// 1. 回波损耗测量\r\n" +
                "   /// 2. 插入损耗测量（或增益测量）\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 对数幅度\r\n" +
                "   /// 数据元-副值 始终为0\r\n" +
                "   /// </summary>\r\n" +
                "   MLOGarithmic\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定相位格式。\r\n" +
                "   /// 单位 度°\r\n" +
                "   /// 应用实例：\r\n" +
                "   /// 测量与线性相位的偏移\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 相位\r\n" +
                "   /// 数据元-副值 始终为0\r\n" +
                "   /// </summary>\r\n" +
                "   PHASe\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定群延迟格式\r\n" +
                "   /// 单位 秒s\r\n" +
                "   /// 应用实例：\r\n" +
                "   /// 测量群时延\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 群时延\r\n" +
                "   /// 数据元-副值 始终为0\r\n" +
                "   /// </summary>\r\n" +
                "   GDELay\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定Smith图表格式（线性/相位）。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 线性幅度\r\n" +
                "   /// 数据元-副值 相位\r\n" +
                "   /// </summary>\r\n" +
                "   SLINear\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定Smith图表格式（对数/相位）。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 对数幅度\r\n" +
                "   /// 数据元-副值 相位\r\n" +
                "   /// </summary>\r\n" +
                "   SLOGarithmic\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定Smith图表格式（实部/虚部）。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 复数的实部\r\n" +
                "   /// 数据元-副值 复数的虚部\r\n" +
                "   /// </summary>\r\n" +
                "   SCOMplex\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定Smith图表格式（R+jX）。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 电阻\r\n" +
                "   /// 数据元-副值 电抗\r\n" +
                "   /// </summary>\r\n" +
                "   SMITh\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定Smith图表格式（G+jB）。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 电导\r\n" +
                "   /// 数据元-副值 电纳\r\n" +
                "   /// </summary>\r\n" +
                "   SADMittance\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定极性格式（Lin/相位）。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 线性幅度\r\n" +
                "   /// 数据元-副值 相位\r\n" +
                "   /// </summary>\r\n" +
                "   PLINear\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定极性格式（对数/相位）。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 对数幅度\r\n" +
                "   /// 数据元-副值 相位\r\n" +
                "   /// </summary>\r\n" +
                "   PLOGarithmic\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定极性格式（实部/虚部）。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 复数实部\r\n" +
                "   /// 数据元-副值 复数实部\r\n" +
                "   /// </summary>\r\n" +
                "   POLar\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定线性幅度格式。\r\n" +
                "   /// 单位 抽象数\r\n" +
                "   /// 应用实例：\r\n" +
                "   /// 测量反射系数\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 线性幅度\r\n" +
                "   /// 数据元-副值 始终为0\r\n" +
                "   /// </summary>\r\n" +
                "   MLINear\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定SWR格式。\r\n" +
                "   /// 单位 抽象数\r\n" +
                "   /// 应用实例：\r\n" +
                "   /// 测量驻波比\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 SWR(驻波比)\r\n" +
                "   /// 数据元-副值 始终为0\r\n" +
                "   /// </summary>\r\n" +
                "   SWR\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定实部格式\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 复数的实部\r\n" +
                "   /// 数据元-副值 始终为0\r\n" +
                "   /// </summary>\r\n" +
                "   REAL\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定虚部格式。\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 复数的虚部\r\n" +
                "   /// 数据元-副值 始终为0\r\n" +
                "   /// </summary>\r\n" +
                "   IMAGinary\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定扩展相位格式。\r\n" +
                "   /// 单位 度°\r\n" +
                "   /// 应用实例：\r\n" +
                "   /// 测量与线性相位的偏移\r\n" +
                "   /// 数据格式：\r\n" +
                "   /// 数据元-主值 扩大的相位\r\n" +
                "   /// 数据元-副值 始终为0\r\n" +
                "   /// </summary>\r\n" +
                "   UPHase\r\n\r\n" +
                "   /// <summary>\r\n" +
                "   /// 指定正相位格式。\r\n" +
                "   /// 单位 度°\r\n" +
                "   /// 应用实例：\r\n" +
                "   /// 测量与线性相位的偏移\r\n" +
                "   /// </summary>\r\n" +
                "   PPHase\r\n";
            return help;
        }

        public static string GetCalStandardHelp()
        {
            return "" +
                "1. 回波损耗：return loss。回波损耗是表示信号反射性能的参数。回波损耗说明入射功率的一部分被反射回到信号源。" +
                "2. 回路损失是指信号在传输线传输时其反射回来的信号量的大小:反射越小,RL值越大" +
                "3. 公式:RL=20|lg(反射功率/入射功率)|";
        }
    }
}
