using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HelpClasses
{
    public class StreamBuilder
    {
        /// <summary>
        /// 对接收到的数据进行解包（将接收到的byte型数组解包为Unicode字符串）
        /// </summary>
        /// <param name="bytes">byte型数组</param>
        /// <returns>Unicode字符串</returns>
        public static string ByteArrayToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte num in bytes)
            {
                sb.Append(num.ToString("X2"));//ToString("X2") 为C#中的字符串格式控制符 
                //X为     十六进制 
                //2为     每次都是两位数

            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns>byte型数组</returns>
        public static byte[] HexToByteArray(string hexStr)
        {
            int len = hexStr.Length / 2;
            byte[] ret = new byte[len];

            for (int i = 0; i < len; i++)
            {
                string str = hexStr.Substring(i * 2, 2);
                ret[i] = Convert.ToByte(str, 16);
            }
            return ret;
        }
    }
}
