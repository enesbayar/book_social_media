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
            var result = fileOperation.WriteToFile(new Domain.User {eMail = "test@gmail.com", Name = "test", UserName = "testUserName", Password = "testpassword"  });
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
            FileResult fileResult = fileOperation.WriteTheBookToTheFile(new Domain.Book {userName = "unitTestUserName", bookName = "unitTestBookName", likeCount = 0, comment = "unitTestComment"  });
            Assert.True(fileResult.isSuccess);
        }

        [Fact]

        public void ReadTheBookFromFileChangeTheLikeCount_Success()
        {

            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "unitTestUserName", bookName = "unitTestBookName", likeCount = 0, comment = "unitTestComment" };
            BookResult bookResult = fileOperation.ReadTheBookFromFileChangeTheLikeCount(book);
            Assert.True(bookResult.isSuccess);
        }

        [Fact]

        public void ReadTheBookFromFileChangeTheLikeCount_BookNotFound()
        {

            FileOperation fileOperation = new FileOperation();
            Book book = new Book { userName = "unitTestUserName", bookName = "unitTestNotFound", likeCount = 0, comment = "unitTestNotFound" };
            BookResult bookResult = fileOperation.ReadTheBookFromFileChangeTheLikeCount(book);
            Assert.False(bookResult.isSuccess);
        }

    }
}
