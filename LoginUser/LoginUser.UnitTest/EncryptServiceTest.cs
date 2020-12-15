using LoginUser.Operation;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace LoginUser.UnitTest
{
    public class EncryptServiceTest
    {

        [Fact]
        public void Encrypt_Success()
        {
            IEncryptService encryptService = new EncryptService();
            var result = encryptService.Encrypt("test");
            var dec = encryptService.Decrypt("test",result);
            Assert.Equal("Password correct", dec);
        }

        [Fact]
        public void Encrypt_Fail()
        {
            IEncryptService encryptService = new EncryptService();
            var result = encryptService.Encrypt("test");
            var dec = encryptService.Decrypt("WrongPassword", result);
            Assert.Equal("Password incorrect", dec);
        }

    }
}
