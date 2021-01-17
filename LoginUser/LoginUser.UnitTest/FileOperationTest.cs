using LoginUser.Domain;
using LoginUser.Operation;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LoginUser.UnitTest
{
    public class FileOperationTest{
        [Fact]
        
        public void WriteToFile_Success() {

            FileOperation fileOperation = new FileOperation();
            var result = fileOperation.WriteToFile(new Domain.User {eMail = "test@gmail.com", Name = "test", UserName = "testUser", Password = "testpassword", likesBookList = "testBook", readBookList = "testBook", CreatedDate = DateTime.Now  });
            Assert.True(result.isSuccess);
        }

        [Fact]
        
        public void ReadFromFile_UserNotFound(){

            FileOperation fileOperation = new FileOperation();
            User user = fileOperation.ReadFromFile("UserNotFound");
            Assert.Null(user.UserName);
        }

        [Fact]

        public void ReadFromFile_Success()
        {

            FileOperation fileOperation = new FileOperation();
            User user = fileOperation.ReadFromFile("testUser");
            Assert.NotNull(user);
        }


        [Fact]

        public void ReadTheBookFromFile_Success()
        {

            FileOperation fileOperation = new FileOperation();
            ArrayList bookList = fileOperation.ReadTheBookFromFile();
            Assert.NotNull(bookList);
        }

        [Fact]

        public void WriteTheBookToFile_Success()
        {

            FileOperation fileOperation = new FileOperation();
            FileResult fileResult = fileOperation.WriteTheBookToTheFile(new Domain.Book {userName = "testUser", bookName = "bookName", likeCount = 0, comment = "unitTestComment" });
            Assert.True(fileResult.isSuccess);
        }

        [Fact]

        public void ReadTheBookFromFileChangeTheLikeCount_Success()
        {

            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "bookName", likeCount = 0, comment = "unitTestComment" };
            User user = new User { eMail = "test@gmail.com", UserName = "testUser",
                Name = "test", Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "testBookName", readBookList = "unitTestBookName"};
            BookResult bookResult = fileOperation.ReadTheBookFromFileChangeTheLikeCount(book,user);
            Assert.True(bookResult.isSuccess);
        }

        [Fact]

        public void ReadTheBookFromFileChangeTheLikeCount_BookNotFound()
        {

            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "unitTestNotFound", likeCount = 0, comment = "unitTestNotFound" };
            User user = new User
            {
                eMail = "NotFound",
                UserName = "NotFound",
                Name = "NotFound",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "unitTestBookName",
                readBookList = "unitTestBookName"
            };
            BookResult bookResult = fileOperation.ReadTheBookFromFileChangeTheLikeCount(book,user);
            Assert.False(bookResult.isSuccess);
        }

        [Fact]

        public void readControl_Success()
        {
            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "testBook", likeCount = 0, comment = "unitTestComment" };
            User user = new User
            {
                eMail = "test@gmail.com",
                UserName = "testUser",
                Name = "test",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "testBook",
                readBookList = "testBook"
            };
            BookResult bookResult = fileOperation.readControl(user, book);
            Assert.True(bookResult.isSuccess);  
        }

        [Fact]

        public void readControl_Fail()
        {
            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "notFound", likeCount = 0, comment = "unitTestComment" };
            User user = new User
            {
                eMail = "test@gmail.com",
                UserName = "testUser",
                Name = "test",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "notFound",
                readBookList = "notFound"
            };
            BookResult bookResult = fileOperation.readControl(user, book);
            Assert.False(bookResult.isSuccess);
        }

        [Fact]

        public void WriteTheReadListToTheFile_Success()
        {
            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "testBook", likeCount = 0, comment = "unitTestComment" };
            User user = new User
            {
                eMail = "test@gmail.com",
                UserName = "testUser",
                Name = "test",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "testBook",
                readBookList = "testBook"
            };
            UserOperationResult userOperationResult = fileOperation.WriteTheReadListToTheFile(user, book);
            Assert.True(userOperationResult.isSuccess);
        }

        [Fact]

        public void WriteTheReadListToTheFile_Fail()
        {
            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "notFound", likeCount = 0, comment = "unitTestComment" };
            User user = new User
            {
                eMail = "test@gmail.com",
                UserName = "userNotFound",
                Name = "test",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "notFound",
                readBookList = "notFound"
            };
            UserOperationResult userOperationResult = fileOperation.WriteTheReadListToTheFile(user, book);
            Assert.False(userOperationResult.isSuccess);
        }

        [Fact]

        public void WriteTheReadListToTheFile_NotYetRead()
        {
            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "testBook", likeCount = 0, comment = "unitTestComment" };
            User user = new User
            {
                eMail = "test@gmail.com",
                UserName = "testUser",
                Name = "test",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "testBook",
                readBookList = "notTestBook"
            };
            UserOperationResult userOperationResult = fileOperation.WriteTheReadListToTheFile(user, book);
            Assert.True(userOperationResult.isSuccess);
        }

        [Fact]
        public void WriteCommentToFile_Success()
        {
            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "bookName", likeCount = 0, comment = "unitTestComment" };
            BookResult bookResult = fileOperation.WriteCommentToFile(book, "newComment");
            Assert.True(bookResult.isSuccess);
        }

        [Fact]
        public void WriteCommentToFile_Fail()
        {
            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "notFound", likeCount = 0, comment = "unitTestComment" };
            BookResult bookResult = fileOperation.WriteCommentToFile(book, "newComment");
            Assert.False(bookResult.isSuccess);
        }

        [Fact]
        public void userInfo_Success()
        {
            FileOperation fileOperation = new FileOperation();
            LoginRequest loginRequest = new LoginRequest {userName = "testUser", 
                password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==" };
            User user = fileOperation.userInfo(loginRequest);
            Assert.NotNull(user);
        }
        [Fact]
        public void userInfo_Fail()
        {
            FileOperation fileOperation = new FileOperation();
            LoginRequest loginRequest = new LoginRequest
            {
                userName = "userNotFound",
                password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw =="
            };
            User user = fileOperation.userInfo(loginRequest);
            Assert.Null(user.UserName);
        }

        [Fact]

        public void ReadTheBookFromFileChangeTheLikeCount_dislike()
        {
            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "testUser", bookName = "bookName", likeCount = 0, comment = "unitTestComment" };
            User user = new User
            {
                eMail = "test@gmail.com",
                UserName = "testUser",
                Name = "test",
                Password = "AQAAAAEAACcQAAAAEOBSnWVetlKxCaWgfosx7VAdI9agxzPPXYcPInGOcI3lEs / pdVd + TsKZdfgut2JHiw ==",
                likesBookList = "bookName",
                readBookList = "bookName"
            };
            BookResult bookResult =fileOperation.ReadTheBookFromFileChangeTheLikeCount(book, user);
            Assert.True(bookResult.isSuccess);
        }
    }
}
