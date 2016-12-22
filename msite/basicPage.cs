using HotCoreUtils.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace msite
{
    public class basicPage: PageBaseHelper
    {
        public const string ENCRYPTKEY = "lechen20";
        /// 验证是否来源于微信
        /// </summary>
        /// <returns></returns>
        public bool ValidateMicroMessenger()
        {
            string agent = HttpContext.Current.Request.UserAgent.ToLower();
            if (!Regex.IsMatch(agent, "MicroMessenger.*", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }
        public string AppId(int customerId)
        {
            return ConfigHelper.GetConfigString("appid");
        }

        public string AppSecret(int customerId)
        {
            return ConfigHelper.GetConfigString("appsecret");
        }

        /// <summary>
        /// 载入错误信息
        /// </summary>
        /// <param name="code">ErrorPageOptions</param>
        /// <param name="desc">The desc.</param>
        /// <returns>System.String.</returns>
        public string LoadErrorNote(string code, string desc)
        {
            string file = string.Format("/error/{0}.html", code);
            string fullfile = HttpContext.Current.Server.MapPath(file);
            if (File.Exists(fullfile))
            {
                string content = File.ReadAllText(fullfile);
                content = content.Replace("{$desc$}", desc);
                return content;
            }
            return desc;
        }

        public  String CreateNoncestr()
        {
            String chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            String res = "";
            Random rd = new Random();
            for (int i = 0; i < 16; i++)
            {
                res += chars[rd.Next(chars.Length - 1)];
            }
            return res;
        }

        public  string FormatBizQueryParaMap(Dictionary<string, string> paraMap,
       bool urlencode, bool tolower = true)
        {

            string buff = "";
            try
            {
                var result = from pair in paraMap orderby pair.Key select pair;
                foreach (KeyValuePair<string, string> pair in result)
                {
                    if (pair.Key != "")
                    {

                        string key = pair.Key;
                        string val = pair.Value;
                        if (urlencode)
                        {
                            val = UrlEncode(val);
                        }
                        if (tolower)
                        {
                            buff += key.ToLower() + "=" + val + "&";
                        }
                        else
                        {
                            buff += key + "=" + val + "&";
                        }

                    }
                }

                if (buff.Length == 0 == false)
                {
                    buff = buff.Substring(0, (buff.Length - 1) - (0));
                }
            }
            catch (Exception)
            {
                // throw new SDKRuntimeException(e.Message);
            }
            return buff;
        }

        public static String Sha1(String s)
        {


            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                    'a', 'b', 'c', 'd', 'e', 'f' };
            try
            {
                //byte[] btInput = System.Text.Encoding.Default.GetBytes(s);
                byte[] btInput = System.Text.Encoding.UTF8.GetBytes(s);//by voidarea

                SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();

                byte[] md = sha.ComputeHash(btInput);
                // 把密文转换成十六进制的字符串形式
                int j = md.Length;
                char[] str = new char[j * 2];
                int k = 0;
                for (int i = 0; i < j; i++)
                {
                    byte byte0 = md[i];
                    str[k++] = hexDigits[(int)(((byte)byte0) >> 4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return new string(str);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }
        private static string UrlEncode(string temp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                string t = temp[i].ToString();
                string k = HttpUtility.UrlEncode(t);
                if (t == k)
                {
                    stringBuilder.Append(t);
                }
                else
                {
                    stringBuilder.Append(k.ToUpper());
                }
            }
            return stringBuilder.ToString();
        }
    }
}