namespace SwitchControl
{
    /// <summary>
    /// Switch操作类，供VNA调用，修改时请勿修改'SwitchControlDll'名称及各方法不能添加参数
    /// </summary>
    public class SwitchControlDll : BasicLibraries.VisaControl.VisaBase.VisaBaseLibrary
    {
        /// <summary>
        /// 连接switch控制器
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            string resources = FindResources(BasicLibraries.VisaControl.VisaBase.ResourceFindPattern.GPIB).Find((x) => { return x.Contains("7"); });
            if (!string.IsNullOrEmpty(resources))
            {
                return OpenResource(resources);
            }
            return false;
        }

        /// <summary>
        /// 使能通道
        /// </summary>
        /// <param name="switchChannel"></param>
        /// <returns></returns>
        public bool Close(int switchChannel)
        {
            Write($"ROUT:CLOSE (@{switchChannel})");
            return true;
        }

        /// <summary>
        /// 断开通道
        /// </summary>
        /// <param name="switchChannel"></param>
        /// <returns></returns>
        public bool Open(int switchChannel)
        {
            Write($"ROUT:OPEN (@{switchChannel})");
            return true;
        }

        /// <summary>
        /// 断开全部通道
        /// </summary>
        /// <returns></returns>
        public bool OpenAll()
        {
            Write("OPEN:ALL");
            return true;
        }
    }
}
