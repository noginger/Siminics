using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;

namespace BaseLibrary.Common
{
    public class Encryption
    {
        private const string KEY_64 = "72B2CBA2";
        private const string IV_64 = "29C9B01D";

        #region 加密字符串
        public string Encode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();

            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
        #endregion

        #region 解密字符串
        public string Decode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return string.Empty;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);

            StreamReader sr = new StreamReader(cst);

            return sr.ReadToEnd();
        }
        #endregion

        #region 加密登录信息
        public string EncodeLogin(LoginInfo loginInfo)
        {
            string loginKey =Encode(loginInfo.UserId.ToString())+"-"+Encode(loginInfo.UserTypeId.ToString())+"-"+Encode(loginInfo.UserName) +"-"+Encode(loginInfo.AreaCode.ToString())+ "-" +Encode(loginInfo.ParentId.ToString())+"-"+Encode(loginInfo.Password) + "-" + Encode(loginInfo.LoginDate.ToString("yyyyMMddHHmm")) + "-" + Encode(loginInfo.IpAddress) + "-" + Encode(loginInfo.LoginType.ToString());

            loginKey = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(loginKey));

            return loginKey;
        }
        #endregion

        #region 解密登录信息
        public LoginInfo DecodeLogin(string loginKey)
        {
            loginKey = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(loginKey));

            string[] arrKey = loginKey.Split('-');

            for (int i = 0; i < arrKey.Length; i++)
                arrKey[i] = Decode(arrKey[i]);

            try
            {
                return new LoginInfo(int.Parse(arrKey[0]),int.Parse(arrKey[1]),arrKey[2], int.Parse(arrKey[3]),int.Parse(arrKey[4]),arrKey[5], DateTime.Parse(arrKey[6].Substring(0, 4) + "-" + arrKey[6].Substring(4, 2) + "-" + arrKey[6].Substring(6, 2) + " " + arrKey[6].Substring(8, 2) + ":" + arrKey[6].Substring(10, 2)), arrKey[7], int.Parse(arrKey[8]));
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 登录信息子类
        [Serializable]
        public class LoginInfo
        {
            public int UserId { get; set; }
            public int UserTypeId { get; set; }
            public string UserName { get; set; }
            public int AreaCode { get; set; }
            public int ParentId { get; set; }
            public string Password { get; set; }
            public DateTime LoginDate { get; set; }
            public string IpAddress { get; set; }
            //0:会员，1：后台
            public int LoginType { get; set; }

            public LoginInfo(int userId,int userTypeId,string userName,int areaCode,int parentId,string password, DateTime loginDate, string ipAddress, int loginType)
            {
                UserId = userId;
                UserTypeId = userTypeId;
                UserName = userName;
                AreaCode = areaCode;
                ParentId = parentId;
                Password = password;
                LoginDate = loginDate;
                IpAddress = ipAddress;
                LoginType = loginType;
            }
        }
        #endregion
    }
}