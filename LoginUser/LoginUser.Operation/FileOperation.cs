using LoginUser.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LoginUser.Operation
{
    public class FileOperation : IFileOperation
    {
        private const string FILE_PATH = "D:\\Ders\\Web Programlama";
        private ArrayList bookList;
        CultureInfo provider = CultureInfo.InvariantCulture;
        public User ReadFromFile(string userName){
            using (StreamReader streamReader = new StreamReader(Path.Combine(FILE_PATH, "Users.txt"), true))
            {
                bool isEndFile = true;
                while (isEndFile)
                {
                    var line = streamReader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    var splittedLine = line.Split(";");
                    if (splittedLine[1] == userName)
                    {
                        return new User { eMail = splittedLine[0], UserName = splittedLine[1], Name = splittedLine[2], Password = splittedLine[3],
                            likesBookList = splittedLine[4], readBookList = splittedLine[5],
                            CreatedDate = DateTime.Parse(splittedLine[6])};

                    }
                }
            }
            return new User {Password = "userNotFound" };
            
        }

        public ArrayList ReadTheBookFromFile()
        {
            bookList = new ArrayList();
            using (StreamReader streamReader = new StreamReader(Path.Combine(FILE_PATH, "Books.txt"), true))
            {
                bool isEndFile = true;
                while (isEndFile)
                {
                    var line = streamReader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    var splittedLine = line.Split(";");
                    bookList.Add(new Book { userName = splittedLine[0], bookName = splittedLine[1], likeCount = int.Parse(splittedLine[2]), comment = splittedLine[3], createdDate = DateTime.ParseExact(splittedLine[4],"MM.dd.yyyy",provider)});
                }
            }
            return bookList; 
        }

        public BookResult ReadTheBookFromFileChangeTheLikeCount(Book book, User user)
        {
            var originalLines = File.ReadAllLines(FILE_PATH + "\\" + "Books.txt");
            var originalUserLines = File.ReadAllLines(FILE_PATH + "\\" + "Users.txt");
            int flag = 0;
            Boolean dislike = false;
            var updatedLines = new List<string>();
            foreach(var originalUserLine in originalUserLines)
            {
                string[] userInfo = originalUserLine.Split(";");
                if (userInfo[1] == user.UserName)
                {
                    user.likesBookList = userInfo[4];
                }
            }
            var likesBookList = user.likesBookList.Split(",");
            foreach(var likesBook in likesBookList)
            {
                if(likesBook == book.bookName)
                {
                    dislike = true;
                }
            }
            foreach (var line in originalLines)
            {
               string[] infos = line.Split(';');
                if (infos[0] == book.userName)
                {
                    if (infos[1] == book.bookName)
                    {
                        if (dislike)
                        {
                            infos[2] = (int.Parse(infos[2]) - 1).ToString();
                            flag = 1;
                        }
                        else
                        {
                            infos[2] = (int.Parse(infos[2]) + 1).ToString();
                            flag = 1;
                        }                     
                    }
                    else
                    {

                    }
                    
                }

                updatedLines.Add(string.Join(";", infos));
            }
            if(flag == 1)
            {
                File.WriteAllLines(FILE_PATH + "\\" + "Books.txt", updatedLines);
                WriteTheLikeListToTheFile(user, book, dislike);
                return new BookResult { isSuccess = true, message = "Success" };
            }
            else
            {
                return new BookResult { isSuccess = false, message = "BookNotFound" };
            }
        }


        public FileResult WriteTheBookToTheFile(Book book)
        {
            using (StreamWriter fileWriter = new StreamWriter(Path.Combine(FILE_PATH, "Books.txt"), true))
            {
                fileWriter.WriteLine(string.Format("{0};{1};{2};{3};{4}", book.userName, book.bookName, book.likeCount, book.comment, book.createdDate.ToString("MM.dd.yyyy")));
            }
            return new FileResult { isSuccess = true };
        }

        public FileResult WriteToFile(User user)
        {

            using (StreamWriter fileWriter = new StreamWriter(Path.Combine(FILE_PATH,"Users.txt"),true))
            {
                fileWriter.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6}",user.eMail, user.UserName, user.Name, user.Password,user.likesBookList,user.readBookList, user.CreatedDate.ToString("MM.dd.yyyy")));
            }
            return new FileResult { isSuccess = true };
        }

        public void WriteTheLikeListToTheFile(User user, Book book, Boolean dislike)
        {
            var originalLines = File.ReadAllLines(FILE_PATH + "\\" + "Users.txt");
            int flag = 0;
            var updatedLines = new List<string>();
            foreach (var line in originalLines)
            {
                string[] infos = line.Split(';');
                if (infos[1] == user.UserName)
                {
                    if (dislike)
                    {
                        string[] likedBooks = infos[4].Split(",");
                        foreach(var likedBook in likedBooks)
                        {
                            if(likedBook == book.bookName)
                            {
                                likedBooks = likedBooks.Where(val => val != likedBook).ToArray();
                            }
                        } 
                        infos[4] = string.Join(",", likedBooks);
                        flag = 1;
                    }
                    else
                    {
                        string[] likedBooks = infos[4].Split(",");
                        Array.Resize(ref likedBooks, likedBooks.Length + 1);
                        likedBooks[likedBooks.Length - 1] = book.bookName;
                        infos[4] = string.Join(",", likedBooks);
                        flag = 1;
                    }

                }
                else
                {

                }
                updatedLines.Add(string.Join(";", infos));
            }
            if(flag == 1)
            {
                File.WriteAllLines(FILE_PATH + "\\" + "Users.txt", updatedLines);
            }
        }

        public BookResult readControl(User user, Book book)
        {
            var originalLines = File.ReadAllLines(FILE_PATH + "\\" + "Users.txt");
            int flag = 0;
            foreach (var originalUserLine in originalLines)
            {
                string[] userInfo = originalUserLine.Split(";");
                if (userInfo[1] == user.UserName)
                {
                    user.readBookList = userInfo[5];
                }  
            }
            var readBookList = user.readBookList.Split(",");
            foreach (var readBook in readBookList)
            {
                if (readBook == book.bookName)
                {
                    flag = 1;
                }
            }
            if(flag == 1)
            {
                return new BookResult { isSuccess = true, message = "alreadyRead" };
            }
            else
            {
                return new BookResult { isSuccess = false, message = "notYetRead" };
            }
        }

        public UserOperationResult WriteTheReadListToTheFile(User user, Book book)
        {
            var originalLines = File.ReadAllLines(FILE_PATH + "\\" + "Users.txt");
            int flag = 0;
            Boolean isRead = false;
            var updatedLines = new List<string>();
            
            foreach (var originalUserLine in originalLines)
            {
                string[] userInfo = originalUserLine.Split(";");
                if (userInfo[1] == user.UserName)
                {
                    user.readBookList = userInfo[5];
                }
            }
            var readBookList = user.readBookList.Split(",");
            foreach (var readBook in readBookList)
            {
                if (readBook == book.bookName)
                {
                    isRead = true;
                }
            }
            foreach (var line in originalLines)
            {
                string[] infos = line.Split(';');
                if (infos[1] == user.UserName)
                {
                    if (isRead)
                    {
                        string[] readBooks = infos[5].Split(",");
                        foreach(var readBook in readBooks)
                        {
                            if (readBook == book.bookName)
                            {
                                readBooks = readBooks.Where(val => val != readBook).ToArray();
                            }
                        }
                        infos[5] = string.Join(",", readBooks);
                        flag = 1;
                    }
                    else
                    {
                        string[] readBooks = infos[5].Split(",");
                        Array.Resize(ref readBooks, readBooks.Length + 1);
                        readBooks[readBooks.Length - 1] = book.bookName;
                        infos[5] = string.Join(",", readBooks);
                        flag = 1;
                    }
                    
                }
                else
                {

                }
                updatedLines.Add(string.Join(";", infos));
            }
            if (flag == 1)
            {
                File.WriteAllLines(FILE_PATH + "\\" + "Users.txt", updatedLines);
                return new UserOperationResult { isSuccess = true, message = "readListUpdated" };
            }
            else
            {
                return new UserOperationResult { isSuccess = false, message = "readListUpdateFail" };
            }
        }

        public BookResult WriteCommentToFile(Book book, string newComment)
        {
            var originalLines = File.ReadAllLines(FILE_PATH + "\\" + "Books.txt");
            int flag = 0;
            var updatedLines = new List<string>();
            foreach (var line in originalLines)
            {
                var infos = line.Split(";");
                if(infos[1] == book.bookName)
                {
                    var commentList = infos[3].Split(",");
                    Array.Resize(ref commentList, commentList.Length + 1);
                    commentList[commentList.Length - 1] = newComment;
                    infos[3] = string.Join(",", commentList);
                    flag = 1;

                }
                else
                {

                }
                updatedLines.Add(string.Join(";", infos));
            }
            if (flag == 1)
            {
                File.WriteAllLines(FILE_PATH + "\\" + "Books.txt", updatedLines);
                return new BookResult { isSuccess = true, message = "commentUpdated" };
            }
            else
            {
                return new BookResult { isSuccess = false, message = "commentUpdateFail" };
            }
        }
        public User userInfo(LoginRequest loginRequest)
        {
            var originalLines = File.ReadAllLines(FILE_PATH + "\\" + "Users.txt");
            foreach(var line in originalLines)
            {
                var infos = line.Split(";");
                if(infos[1] == loginRequest.userName) {
                    return new User
                    {
                        eMail = infos[0],
                        UserName = infos[1],
                        Name = infos[2],
                        Password = infos[3],
                        likesBookList = infos[4],
                        readBookList = infos[5],
                        CreatedDate = DateTime.Parse(infos[6])
                    };
                }
                else
                {

                }
            }
            return new User{ };
        }
    }
}
