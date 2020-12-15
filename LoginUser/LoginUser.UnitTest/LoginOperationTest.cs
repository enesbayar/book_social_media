using LoginUser.Domain;
using LoginUser.Operation;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LoginUser.UnitTest
{
    public class LoginOperationTest{
        
        [Fact]
        
        public void Login_Succes(){

            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            mockEncrypt.Setup(x => x.Encrypt("successful")).Returns("successful");
            mockEncrypt.Setup(x => x.Decrypt(It.IsAny<String>(), It.IsAny<String>())).Returns("Password correct");
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.ReadFromFile(It.IsAny<String>())).Returns(new User { UserName = "testUser", Password = "successful" });

            LoginOperation loginOperation = new LoginOperation(mockEncrypt.Object, mockFile.Object);
            LoginRequest loginRequest = new LoginRequest { userName = "testUser", password = "successful"};
            var result = loginOperation.Login(loginRequest);
            
            Assert.True(result.isSuccess);
        }

        [Fact]

        public void Login_WrongPassword()
        {

            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            mockEncrypt.Setup(x => x.Encrypt("successful")).Returns("successful");
            mockEncrypt.Setup(x => x.Decrypt(It.IsAny<String>(), It.IsAny<String>())).Returns("successful");
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.ReadFromFile(It.IsAny<String>())).Returns(new User { UserName = "testUser", Password = "successful" });

            LoginOperation loginOperation = new LoginOperation(mockEncrypt.Object, mockFile.Object);
            LoginRequest loginRequest = new LoginRequest { userName = "testUser", password = "Wrong Password" };
            var result = loginOperation.Login(loginRequest);

            Assert.False(result.isSuccess);
            Assert.Equal("Wrong Password", result.message);
        }

        [Fact]

        public void Login_UserNotFound()
        {

            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            mockEncrypt.Setup(x => x.Encrypt("successful")).Returns("successful");
            mockEncrypt.Setup(x => x.Decrypt(It.IsAny<String>(), It.IsAny<String>())).Returns("successful");
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.ReadFromFile(It.IsAny<String>())).Returns(new User {Password = "successful" });

            LoginOperation loginOperation = new LoginOperation(mockEncrypt.Object, mockFile.Object);
            LoginRequest loginRequest = new LoginRequest { userName = "UserNotFound", password = "Wrong successful" };
            var result = loginOperation.Login(loginRequest);

            Assert.False(result.isSuccess);
            Assert.Equal("User Not Found", result.message);
        }
    }
}
