using LoginUser.Domain;
using System;
using System.Collections;

namespace LoginUser.Operation
{
    public interface IFileOperation
    {
        FileResult WriteToFile(User user);

        FileResult WriteTheBookToTheFile(Book book);
        User ReadFromFile(string userName);

        ArrayList ReadTheBookFromFile();

        BookResult ReadTheBookFromFileChangeTheLikeCount(Book book, User user);

        UserOperationResult WriteTheReadListToTheFile(User user, Book book);

        void WriteTheLikeListToTheFile(User user, Book book, Boolean dislike);
        BookResult WriteCommentToFile(Book book, string newComment);
        BookResult readControl(User user, Book book);
        User userInfo(LoginRequest loginRequest);

    }
}