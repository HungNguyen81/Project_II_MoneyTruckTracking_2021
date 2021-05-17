using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MoneyTruckTrackingAPI.Helpers
{
    public class HashingHelper
    {
        public static string HashUsingPbkdf2(string password, string salt)
        {
            var bytes = new Rfc2898DeriveBytes(
                                password,
                                Convert.FromBase64String(salt),
                                10000,
                                HashAlgorithmName.SHA256
                                );
            var derivedRandomKey = bytes.GetBytes(32);
            return Convert.ToBase64String(derivedRandomKey);
        }
    }
}
