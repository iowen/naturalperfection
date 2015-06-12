using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace natp.Models
{
    public class MediaNameGen
    {
        public static string GetRandomMediaName()
        {
            string text = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            using (MD5 mD = MD5.Create())
            {
                string md5Hash = MediaNameGen.GetMd5Hash(mD, DateTime.UtcNow.ToLongDateString() + DateTime.UtcNow.ToLongTimeString());
                text += md5Hash;
                text += Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            }
            return text;
        }
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] array = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
