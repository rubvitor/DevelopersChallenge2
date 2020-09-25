using System;
using System.Security.Cryptography;
using System.Text;

namespace FinantialManager.Infra.CrossCutting.Util.Extensions
{
    public static class StringExtensions
    {
        public static Guid GetGuid(this string input)
        {
            return new Guid(OFXUtils.GetHash(input));
        }

        public static string GetHash(this string input)
        {
            return OFXUtils.GetHash(input);
        }
    }
}
