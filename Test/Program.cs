using System;

namespace Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            VNAControl.VNA.VNACommonData.ShowVNAMainForm();
#if false
            // 1. 通过配置文件获取有多少VNA
            VNAControl.VNA.VNACommonData.GetVNAAllParameters(out VNAAllParameter[] allParameters);

            // 2. 实例化
            VNABase[] vNAs = new VNABase[allParameters.Length];
            for (int i = 0; i < allParameters.Length; i++)
            {
                switch (allParameters[i].Type)
                {
                    case VNAType.ZNB8:
                        vNAs[i] = new RS_ZNB8();
                        break;
                    case VNAType.E5071C:
                        vNAs[i] = new KS_E5071C();
                        break;
                    default:
                        break;
                }
                vNAs[i].Connect(allParameters[i].VNAIP, out string info);
            }

            // 3. 触发测试获取数据
            VNADataFormat dataFormat = new VNADataFormat();
            vNAs[0].GetData(allParameters[0].VNAChannelDatas[0], allParameters[0].VNAChannelDatas.Count, ref dataFormat);
#endif
            Console.ReadLine();
        }
    }
}
