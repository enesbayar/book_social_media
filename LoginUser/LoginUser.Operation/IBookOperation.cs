using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LoginUser.Domain;

namespace LoginUser.Operation
{
    public interface IBookOperation
    {
        BookResult CreateBook(Book book);
        ArrayList ReadBook();
        BookResult incrementLikeCount(Book book, User user);
        BookResult WriteCommentToFile(Book book, string newComment);
        BookResult readControl(User user, Book book);
    }
}
