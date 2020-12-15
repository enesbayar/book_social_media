using LoginUser.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LoginUser.Operation
{
    public class BookOperation : IBookOperation
    {
        private ArrayList bookList;
        private readonly IFileOperation fileOperation;

        public BookOperation(IFileOperation fileOperation)
        {
            this.fileOperation = fileOperation;
        }
        public BookResult CreateBook(Book book)
        {
            if (string.IsNullOrEmpty(book.userName))
            {
                return new BookResult { isSuccess = false, message = "Empty UserName" };
            }
            else if (string.IsNullOrEmpty(book.bookName))
            {
                return new BookResult { isSuccess = false, message = "Empty BookName" };
            }
            
            else
            {

                book.likeCount = 0;
                fileOperation.WriteTheBookToTheFile(book);

                return new BookResult { isSuccess = true, message = "Book Created" };
            }
        }

        public ArrayList ReadBook()
        {

            bookList = fileOperation.ReadTheBookFromFile();

            return bookList;
        }

        public BookResult incrementLikeCount(Book book)
        {
            BookResult bookResult = fileOperation.ReadTheBookFromFileChangeTheLikeCount(book);
            return bookResult;
        }
    }
}
