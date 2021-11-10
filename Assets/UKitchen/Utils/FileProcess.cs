using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UKitchen.Utils
{
    public static class FileProcess
    {
        public static void EncodeFileAsBinary(string val, string path, string key = "PASsWord")
        {

            FileStream stream = new FileStream(path, 
                FileMode.OpenOrCreate,FileAccess.Write);

            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);

            CryptoStream crStream = new CryptoStream(stream,
                cryptic.CreateEncryptor(),CryptoStreamMode.Write);


            byte[] data = ASCIIEncoding.ASCII.GetBytes(val);

            crStream.Write(data,0,data.Length);

            crStream.Close();
            stream.Close();
        }

        public static string DecodeFileFromBinary(string path, string key = "PASsWord")
        {
            if (File.Exists(path))
            {

                FileStream stream = new FileStream(path, 
                    FileMode.Open,FileAccess.Read);

                DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

                cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
                cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);

                CryptoStream crStream = new CryptoStream(stream,
                    cryptic.CreateDecryptor(),CryptoStreamMode.Read);

                StreamReader reader = new StreamReader(crStream);

                string data = reader.ReadToEnd();

                reader.Close();
                stream.Close();

                return data;
            }
            else
                return string.Empty;
        }
    }
}