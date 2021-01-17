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
    public class BookOperationTest
    {
        [Fact]

        public void CreateBook_Success()
        {
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.WriteTheBookToTheFile(It.IsAny<Domain.Book>())).Returns(new Domain.FileResult { isSuccess = true });

            BookOperation bookOperation = new BookOperation(mockFile.Object);

            BookResult result = bookOperation.CreateBook(new Book { userName = "bookUserNameTest", bookName = "enesbayar", likeCount = 0, comment = "test" });

            Assert.True(result.isSuccess);
        }

        [Fact]
        public void CreateBook_UserName_Empty()
        {

            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();

            BookOperation bookOperation = new BookOperation(mockFile.Object);

            BookResult result = bookOperation.CreateBook(new Book { userName = "",});

            Assert.Equal("Empty UserName", result.message);
        }


        [Fact]
        public void CreateBook_BookName_Empty()
        {

            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();

            BookOperation bookOperation = new BookOperation(mockFile.Object);

            BookResult result = bookOperation.CreateBook(new Book { userName = "testUserName", bookName = "" });

            Assert.Equal("Empty BookName", result.message);
        }

        [Fact]
        public void ReadBook_Success()
        {

            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.ReadTheBookFromFile()).Returns(new ArrayList { new Book {userName = "testAListUserName", bookName = "testALBookName", likeCount = 0, comment = "testALComment" },
            new Book{ userName = "testAListUserName2", bookName = "testALBookName2", likeCount = 0, comment = "testALComment2" } });
            BookOperation bookOperation = new BookOperation(mockFile.Object);

            ArrayList arrayListResult = bookOperation.ReadBook();

            Assert.NotNull(arrayListResult);
        }


        [Fact]
        public void incrementLikeCount_Success()
        {

            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.ReadTheBookFromFileChangeTheLikeCount(It.IsAny<Domain.Book>(),It.IsAny<Domain.User>())).Returns(new BookResult { isSuccess = true, message = "success"});
            BookOperation bookOperation = new BookOperation(mockFile.Object);
            Book book = new Book();
            User user = new User();
            BookResult result = bookOperation.incrementLikeCount(book,user);

            Assert.True(result.isSuccess);
        }

        [Fact]
        public void writeCommentToFile_Success()
        {
            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.WriteCommentToFile(It.IsAny<Domain.Book>(), It.IsAny<string>())).Returns(new BookResult { isSuccess = true, message = "success" });
            BookOperation bookOperation = new BookOperation(mockFile.Object);
            Book book = new Book();
            BookResult result = bookOperation.WriteCommentToFile(book, "testComment");
            Assert.True(result.isSuccess);
        }

        [Fact]
        public void readControl_Success()
        {

            Mock<IFileOperation> mockFile = new Mock<IFileOperation>();
            mockFile.Setup(x => x.readControl(It.IsAny<Domain.User>(), It.IsAny<Domain.Book>())).Returns(new BookResult { isSuccess = true, message = "success" });
            BookOperation bookOperation = new BookOperation(mockFile.Object);
            Book book = new Book();
            User user = new User();
            BookResult result = bookOperation.readControl(user, book);

            Assert.True(result.isSuccess);
        }
    }
}
