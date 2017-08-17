using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cee.Tools.DEncrypt
{
    public class DEncrypt
    {

        #region 使用 缺省密钥字符串 加密/解密string

        /// <summary>
        /// 使用缺省密钥字符串加密string
        /// </summary>
        /// <param name="original">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, "WEMEit@201314");
        }
        /// <summary>
        /// 使用缺省密钥字符串解密string
        /// </summary>
        /// <param name="original">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, "WEMEit@201314", System.Text.Encoding.Default);
        }

        #endregion

        #region 使用 给定密钥字符串 加密/解密string
        /// <summary>
        /// 使用给定密钥字符串加密string
        /// </summary>
        /// <param name="original">原始文字</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original, string key)
        {
            byte[] buff = System.Text.Encoding.Default.GetBytes(original);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }
        /// <summary>
        /// 使用给定密钥字符串解密string
        /// </summary>
        /// <param name="original">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, System.Text.Encoding.Default);
        }

        /// <summary>
        /// 使用给定密钥字符串解密string,返回指定编码方式明文
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }
        #endregion

        #region 使用 缺省密钥字符串 加密/解密/byte[]
        /// <summary>
        /// 使用缺省密钥字符串解密byte[]
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("LITIANPING");
            return Decrypt(encrypted, key);
        }
        /// <summary>
        /// 使用缺省密钥字符串加密
        /// </summary>
        /// <param name="original">原始数据</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("LITIANPING");
            return Encrypt(original, key);
        }
        #endregion

        #region  使用 给定密钥 加密/解密/byte[]

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <returns>摘要</returns>
        public static byte[] MakeMD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }


        /// <summary>
        /// 使用给定密钥加密
        /// </summary>
        /// <param name="original">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        /// <summary>
        /// 使用给定密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        #endregion

        #region MD5加密

        #region MD5算法加密字符串( 16位 )
        /// <summary>
        /// MD5算法加密字符串( 16位 )
        /// </summary>
        /// <param name="text">要加密的字符串</param>    
        public static string MD5By16(string text)
        {
            //创建MD5密码服务提供程序
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //获取加密字符串
            string result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(text)), 4, 8);

            //释放资源
            md5.Clear();

            //返回MD5值的字符串表示
            return result.Replace("-", "");
        }
        #endregion

        #region MD5算法加密字符串( 32位 )

        #region 重载1
        /// <summary>
        /// MD5算法加密字符串( 32位 )
        /// </summary>
        /// <param name="text">要加密的字符串</param>    
        /// <param name="encoding">字符编码</param>    
        public static string MD5By32(string text, Encoding encoding)
        {
            //创建MD5密码服务提供程序
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //计算传入的字节数组的哈希值
            byte[] hashCode = md5.ComputeHash(encoding.GetBytes(text));

            //释放资源
            md5.Clear();

            //返回MD5值的字符串表示
            string temp = "";
            for (int i = 0, len = hashCode.Length; i < len; i++)
            {
                temp += hashCode[i].ToString("x").PadLeft(2, '0');
            }
            return temp;
        }
        #endregion

        #region 重载2
        /// <summary>
        /// MD5算法加密字符串( 32位 )
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        public static string MD5By32(string text)
        {
            return MD5By32(text, Encoding.UTF8);
        }
        #endregion

        #region 重载3
        /// <summary>
        /// MD5算法加密字符串( 支付宝专用 )
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        public static string MD5ByAlipay(string text)
        {
            return MD5By32(text, Encoding.GetEncoding("gb2312"));
        }
        #endregion

        #endregion

        #endregion 

        #region HMACSHA1加密
        public static string HMACSHA1(string key, string data)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException();
            }
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(data);

            HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return ToHex(hashBytes);
        }

        public static string ToHex(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            byte b;
            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }
            return new string(c);
        }
        #endregion
    }
}
