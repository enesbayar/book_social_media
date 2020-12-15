using LoginUser.Domain;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace LoginUser.Operation
{
    public class FileOperation : IFileOperation
    {
        private const string FILE_PATH = "D:\\Ders\\Web Programlama";
        private ArrayList bookList;
        private List<string> updatedLines;

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
                        return new User { eMail = splittedLine[0], UserName = splittedLine[1], Name = splittedLine[2], Password = splittedLine[3] };

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
      
                    bookList.Add(new Book { userName = splittedLine[0], bookName = splittedLine[1], likeCount = int.Parse(splittedLine[2]), comment = splittedLine[3] });
                }
            }
            return bookList; 
        }

        public BookResult ReadTheBookFromFileChangeTheLikeCount(Book book)
        {
            var originalLines = File.ReadAllLines(FILE_PATH + "\\" + "Books.txt");
            int flag = 0;
            var updatedLines = new List<string>();
            foreach (var line in originalLines)
            {
               string[] infos = line.Split(';');
                if (infos[0] == book.userName)
                {
                    if (infos[1] == book.bookName)
                    {
                        // update value
                        infos[2] = (int.Parse(infos[2]) + 1).ToString();
                        flag = 1;
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
                fileWriter.WriteLine(string.Format("{0};{1};{2};{3}", book.userName, book.bookName, book.likeCount, book.comment));
            }
            return new FileResult { isSuccess = true };
        }

        public FileResult WriteToFile(User user)
        {

            using (StreamWriter fileWriter = new StreamWriter(Path.Combine(FILE_PATH,"Users.txt"),true))
            {
                fileWriter.WriteLine(string.Format("{0};{1};{2};{3};{4}",user.eMail, user.UserName, user.Name, user.Password, user.CreatedDate.ToString("MM.dd.yyyy")));
            }
            return new FileResult { isSuccess = true };
        }

    }
}
