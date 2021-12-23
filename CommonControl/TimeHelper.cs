namespace BasicLibraries.CommonControl.Time
{
    using System;
    public static class TimeHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static double GetTimeStamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (DateTime.Now - startTime).TotalSeconds;
        }

        /// <summary>
        /// 时间戳转换为时间
        /// </summary>
        /// <param name="timeStamp">时间截</param>
        /// <returns></returns>
        public static DateTime TimeStampToDateTime(double timeStamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timeStamp).ToLocalTime();
        }
    }
}
