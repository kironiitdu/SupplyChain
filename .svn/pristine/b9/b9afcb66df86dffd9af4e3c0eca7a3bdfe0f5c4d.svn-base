using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DMSApi.Models.Repository
{
    public class PasswordHash
    {
        /// <summary>
        /// Maruf: 30-May-2016
        /// Functions: This class generates hases for password and verify hashed password with hashed password saved in database,
        /// Verification process purposefully delays to give the hacker a shitfull of pain
        /// </summary>
        static Random rnd = new Random();
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        // public static int Pbkdf2Iterations = rnd.Next(2000, 3000); // Maruf: 21.Jun.2017
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        public static string HashPassword(string password)
        {
            int pbkdf2Iterations = rnd.Next(2000, 3000);
            var cryptoProvider = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);  // Maruf: Fill the array with a random value.
            var hash = GetPbkdf2Bytes(password, salt, pbkdf2Iterations, HashByteSize);
            return pbkdf2Iterations + "|" +
                   Convert.ToBase64String(salt) + "|" +
                   Convert.ToBase64String(hash);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { '|' };
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}