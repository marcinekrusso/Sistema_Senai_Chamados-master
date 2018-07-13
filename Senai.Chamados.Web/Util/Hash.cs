using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
namespace Senai.Chamados.Web.Util
{
    public class Hash
    {

        public static object StringBuider { get; private set; }

        public static string GeraHash(string Texto)
        {
            StringBuilder result = new StringBuilder();
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(Texto);
            byte[] hash = sha256.ComputeHash(bytes);

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X"));
            }

            return result.ToString();
        }
    }
}