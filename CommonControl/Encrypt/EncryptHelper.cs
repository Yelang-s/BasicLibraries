namespace BasicLibraries.CommonControl.DES
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    /// <summary>
    /// DES
    /// 双向，可解密
    /// </summary>
    public class DESHelper
    {
        /// <summary>
        /// 获得一个字符串的加密密文
        /// </summary>
        /// <param name="originalString">明文字符串</param>
        /// <param name="key">加密/解密密钥</param>
        /// <returns>密文字符串</returns>
        public static string EncryptString(string originalString, string key)
        {
            string result = "";
            if (string.IsNullOrEmpty(originalString)) return result;

            try
            {
                string keyStr = key + "HZ";

                //明文
                //byte[] srcData = System.Text.ASCIIEncoding.ASCII.GetBytes(plainText);
                byte[] srcData = Encoding.Unicode.GetBytes(originalString);

                MemoryStream sin = new MemoryStream();
                //将明文写入内存
                sin.Write(srcData, 0, srcData.Length);
                sin.Position = 0;

                MemoryStream sout = new MemoryStream();
                DES des = new DESCryptoServiceProvider();

                //得到密钥
                string sTemp;
                if (des.LegalKeySizes.Length > 0)
                {
                    int lessSize = 0, moreSize = des.LegalKeySizes[0].MinSize;

                    while (keyStr.Length * 8 > moreSize &&
                        des.LegalKeySizes[0].SkipSize > 0 &&
                        moreSize < des.LegalKeySizes[0].MaxSize)
                    {
                        lessSize = moreSize;
                        moreSize += des.LegalKeySizes[0].SkipSize;
                    }

                    if (keyStr.Length * 8 > moreSize)
                        sTemp = keyStr.Substring(0, (moreSize / 8));
                    else
                        sTemp = keyStr.PadRight(moreSize / 8, ' ');
                }
                else
                    sTemp = keyStr;

                //设置密钥
                des.Key = Encoding.ASCII.GetBytes(sTemp);

                //设置初始化向量
                if (keyStr.Length > des.IV.Length)
                {
                    des.IV = Encoding.ASCII.GetBytes(keyStr.Substring(0, des.IV.Length));
                }
                else
                {
                    des.IV = Encoding.ASCII.GetBytes(keyStr.PadRight(des.IV.Length, ' '));
                }

                //加密流
                CryptoStream encStream = new CryptoStream(sout, des.CreateEncryptor(), CryptoStreamMode.Write);

                //明文流程的长度
                long lLen = sin.Length;
                //已经读取长度
                int nReadTotal = 0;

                //读入块
                byte[] buf = new byte[8];

                int nRead;

                //从明文流读到加密流中
                while (nReadTotal < lLen)
                {
                    nRead = sin.Read(buf, 0, buf.Length);
                    encStream.Write(buf, 0, nRead);
                    nReadTotal += nRead;
                }
                encStream.Close();

                //密文
                result = Convert.ToBase64String(sout.ToArray());
            }
            catch { }

            return result;
        }

        /// <summary>
        /// 对加密密文进行解密
        /// </summary>
        /// <param name="encryptedString">待解密的密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文字符串</returns>
        public static string DecryptString(string encryptedString, string key)
        {
            string result = "";
            if (string.IsNullOrEmpty(encryptedString)) return result;

            try
            {
                string keyStr = key + "HZ";
                //密文
                byte[] encData = Convert.FromBase64String(encryptedString);

                //将密文写入内存
                MemoryStream sin = new MemoryStream(encData);

                MemoryStream sout = new MemoryStream();

                DES des = new DESCryptoServiceProvider();

                //得到密钥
                string sTemp;
                if (des.LegalKeySizes.Length > 0)
                {
                    int lessSize = 0, moreSize = des.LegalKeySizes[0].MinSize;

                    while (keyStr.Length * 8 > moreSize &&
                        des.LegalKeySizes[0].SkipSize > 0 &&
                        moreSize < des.LegalKeySizes[0].MaxSize)
                    {
                        lessSize = moreSize;
                        moreSize += des.LegalKeySizes[0].SkipSize;
                    }

                    if (keyStr.Length * 8 > moreSize)
                        sTemp = keyStr.Substring(0, (moreSize / 8));
                    else
                        sTemp = keyStr.PadRight(moreSize / 8, ' ');
                }
                else
                    sTemp = keyStr;

                //设置密钥
                des.Key = Encoding.ASCII.GetBytes(sTemp);

                //设置初始化向量
                if (keyStr.Length > des.IV.Length)
                {
                    des.IV = Encoding.ASCII.GetBytes(keyStr.Substring(0, des.IV.Length));
                }
                else
                {
                    des.IV = Encoding.ASCII.GetBytes(keyStr.PadRight(des.IV.Length, ' '));
                }

                //解密流

                CryptoStream decStream = new CryptoStream(sin, des.CreateDecryptor(), CryptoStreamMode.Read);

                //密文流的长度
                long lLen = sin.Length;
                //已经读取长度
                int nReadTotal = 0;

                //读入块
                byte[] buf = new byte[8];

                int nRead;
                //从密文流读到解密流中
                while (nReadTotal < lLen)
                {
                    nRead = decStream.Read(buf, 0, buf.Length);
                    if (0 == nRead) break;

                    sout.Write(buf, 0, nRead);
                    nReadTotal += nRead;
                }
                decStream.Close();

                //明文
                UnicodeEncoding ascEnc = new UnicodeEncoding();
                result = ascEnc.GetString(sout.ToArray());
            }
            catch { }

            return result;
        }
    }
}
