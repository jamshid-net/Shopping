using Shopping.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Shopping.Application.Service
{
    public class HashStringService : IHashStringService
    {
        public Task<string> HashStringAsync(string hashString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(hashString);
                byte[] hashbytes = sha256.ComputeHash(bytes);
                hashString = Convert.ToBase64String(hashbytes);

            }
            return Task.FromResult(hashString);
        }
    }
}
