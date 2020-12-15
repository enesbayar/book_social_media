using LoginUser.Domain;
using LoginUser.Operation;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LoginUser.UnitTest
{
    public class UserOperationTest{
        [Fact]
        
        public void CreateUser_Success() {
            
            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            mockEncrypt.Setup(x => x.Encrypt("test")).Returns("successful");
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.WriteToFile(It.IsAny<Domain.User>())).Returns(new Domain.FileResult { isSuccess = true });

            UserOperation userOperation = new UserOperation(mockEncrypt.Object, mockFile.Object);

            UserOperationResult result = userOperation.CreateUser(new User { eMail = "enesbayar@gmail.com", UserName = "enesbayar", Name = "enes", Password = "test" });

            Assert.True(result.isSuccess);
            Assert.Equal(DateTime.Today.Day ,result.createdDate.Day);
        }
        [Fact]
        public void CreateUser_UserName_Empty() {
            
            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();

            UserOperation userOperation = new UserOperation(mockEncrypt.Object, mockFile.Object);

            UserOperationResult result = userOperation.CreateUser(new User { UserName = "" });

            Assert.Equal("Empty UserName", result.message);
        }

        [Fact]
        public void CreateUser_Name_Empty()
        {

            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();

            UserOperation userOperation = new UserOperation(mockEncrypt.Object, mockFile.Object);

            UserOperationResult result = userOperation.CreateUser(new User { Name = "" , UserName = "test"});

            Assert.Equal("Empty Name", result.message);
        }

        [Fact]
        public void CreateUser_Email_Empty()
        {

            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();

            UserOperation userOperation = new UserOperation(mockEncrypt.Object, mockFile.Object);

            UserOperationResult result = userOperation.CreateUser(new User { eMail = "", Name = "test", UserName = "test" });

            Assert.Equal("Empty eMail", result.message);
        }

        [Fact]
        public void CreateUser_Password_Empty()
        {

            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();

            UserOperation userOperation = new UserOperation(mockEncrypt.Object, mockFile.Object);

            UserOperationResult result = userOperation.CreateUser(new User {Password ="", eMail = "test@gmail.com",Name = "test", UserName = "test" });

            Assert.Equal("Empty Password", result.message);
        }
    }
}
