using LoginUser.Domain;
using Microsoft.AspNetCore.Identity;
using System;

namespace LoginUser.Operation
{
    public class EncryptService : IEncryptService
    {

        public string Decrypt(string password, string hashedPassword)
        {
            var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(null, hashedPassword, password);
            switch (passwordVerificationResult)
            {
                case Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed:
                    return ("Password incorrect");
                    

                case Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success:
                    return ("Password correct");


                default:
                    throw new ArgumentNullException();
            }

        }

        public string Encrypt(string password)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(null, password);
            return hashedPassword;
        }

    }
}
