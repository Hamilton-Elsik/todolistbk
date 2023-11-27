using System.Security.Cryptography;
using System.Text;

namespace ToDoListBk.Utils;

public class Encriptar
{
    public static string EncriptarPassword(string input)
    {
        MD5 mD5Hash = MD5.Create();

        byte[] data = mD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();


    }
}
