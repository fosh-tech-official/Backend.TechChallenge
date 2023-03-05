using System;
using Backend.TechChallenge.Api.Domain.Model;

namespace Backend.TechChallenge.Api.Helper
{
    public static class UserHelper
    {
        public static void NormalizeEmail(UserBase user)
        {
            var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            user.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
