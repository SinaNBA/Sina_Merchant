using System.Security.Cryptography;
using System.Text;

namespace SinaMerchant.Web.Services
{
    public class MD5PasswordHelper : IPasswordHelper
    {
        public string HashPassword(string password)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
            md5 = MD5.Create();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string   
            return BitConverter.ToString(encodedBytes);
        }
    }
    
}
