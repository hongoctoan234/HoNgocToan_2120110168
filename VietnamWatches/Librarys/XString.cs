using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ThietBiDienTu
{
    public static class XString
    {
        public static string str_Slug(string s)
        {

            String[][] symbols =
            {
                new String[] { "[åäāàáạảãâầấậẩẫăằắặẳẵäą]", "a" },
                new String[] { "[đďḋđ]", "d" },
                new String[] { "[èéẹẻẽêềếệểễęëěė]", "e" },
                new String[] { "[ìíîịỉĩïîī¡į]", "i" },
                new String[] { "[öòóọỏõôồốộổỗơờớợởỡöøō]", "o" },
                new String[] { "[üùúūụủũưừứựửữüųů]", "u" },
                new String[] { "[\\s'\";,]", "-" }
            };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static string str_Limit(this string s, int limit)
        {
            int length = (limit < 20) ? 30 : limit;
            if (s.Length > length)
            {
                s = s.Substring(0, limit) + ".....";
            }
            return s;
        }
        public static string GetMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }

            return sbHash.ToString();

        }
        public static string ToMD5(this string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }
            return sbHash.ToString();

        }
    }
}