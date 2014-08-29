using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Persistent;
using Model;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void TestSHA1()
        {
            byte[] data = System.Text.Encoding.Default.GetBytes("I am not a robot");//以字节方式存储
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] result = sha1.ComputeHash(data);//得到哈希值
            string r1 = System.BitConverter.ToString(result).Replace("-", ""); //转换成为字符串的显示
            Console.WriteLine(r1);
            string r2 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("I am not a robot", "SHA1");
            Console.WriteLine(r2);
            Console.ReadLine();
        }

       
    }
}
