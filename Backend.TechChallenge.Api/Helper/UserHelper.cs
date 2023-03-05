using System;
using Backend.TechChallenge.Api.Domain.Model;

namespace Backend.TechChallenge.Api.Helper
{
    public static class UserHelper
    {
        // Check this out to improve
        // https://dev.maxmind.com/minfraud/normalizing-email-addresses-for-minfraud?lang=en
        public static void NormalizeEmail(UserBase user)
        {
            var email = user.Email;
            email = email.Trim();
            email = email.ToLower();
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < aux.Length; i++)
                aux[i] = aux[i].Trim();
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            user.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
