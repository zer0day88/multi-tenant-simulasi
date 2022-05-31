using System;
using System.Linq;

namespace PIS.API.Helper
{
    public static class StringHelper
    {
        /// <summary>
        /// method to generate random string(plain text password) for reset password
        /// </summary>
        /// <param name="length">length of generated plain text password </param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}