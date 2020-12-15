using LoginUser.Domain;
using System.Collections;

namespace LoginUser.Operation
{
    public interface IFileOperation
    {
        FileResult WriteToFile(User user);

        FileResult WriteTheBookToTheFile(Book book);
        User ReadFromFile(string userName);

        ArrayList ReadTheBookFromFile();

        BookResult ReadTheBookFromFileChangeTheLikeCount(Book book);

    }
}