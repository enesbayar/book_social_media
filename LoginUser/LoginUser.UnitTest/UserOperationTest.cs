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

        [Fact]
        public void readListUpdate_Success()
        {

            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            mockEncrypt.Setup(x => x.Encrypt("test")).Returns("successful");
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.WriteTheReadListToTheFile(It.IsAny<Domain.User>(), It.IsAny<Domain.Book>())).Returns(new Domain.UserOperationResult { isSuccess = true, 
                message = "readListUpdated" });
    
            UserOperation userOperation = new UserOperation(mockEncrypt.Object, mockFile.Object);

            Book book = new Book { userName = "unitTestUserName", bookName = "unitTestBookName", likeCount = 0, comment = "unitTestComment" };
            User user = new User
            {
                eMail = "test@gmail.com",
                UserName = "testUserName",
                Name = "test",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "unitTestBookName",
                readBookList = "unitTestBookName"
            };

            UserOperationResult result = userOperation.readListUpdate(user, book);

            Assert.Equal("readListUpdated", result.message);
        }

        [Fact]
        public void userInfo_Success()
        {

            Mock<IEncryptService> mockEncrypt = new Mock<IEncryptService>();
            mockEncrypt.Setup(x => x.Encrypt("test")).Returns("successful");
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.userInfo(It.IsAny<Domain.LoginRequest>())).Returns(new User
            {
                eMail = "test@gmail.com",
                UserName = "testUserName",
                Name = "test",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "unitTestBookName",
                readBookList = "unitTestBookName"
            });

            UserOperation userOperation = new UserOperation(mockEncrypt.Object, mockFile.Object);
            LoginRequest loginRequest = new LoginRequest
            {
                userName = "testUserName",
                password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
            };

            User user = userOperation.userInfo(loginRequest);

            Assert.NotNull(user);
        }
    }
}
