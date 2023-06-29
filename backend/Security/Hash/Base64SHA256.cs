using System.Security.Cryptography;
using System.Text;


namespace backend.Security.Hash;
public class Base64SHA256 : IHashAlgoritm
{
    public string ToHash(string str)
    {
         using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(str);
        var hasBytes = sha.ComputeHash(bytes);
        var hash = Convert.ToBase64String(hasBytes);

        return hash;
    }
}