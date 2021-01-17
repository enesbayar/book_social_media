using LoginUser.Domain;
using System;

namespace LoginUser.Operation
{
    public class UserOperation : IUserOperation
    {
        private readonly IEncryptService encryptService;
        private readonly IFileOperation fileOperation;

        public UserOperation(IEncryptService encryptService, IFileOperation fileOperation)
        {
            this.encryptService = encryptService;
            this.fileOperation = fileOperation;
        }
        public UserOperationResult CreateUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName)) {
                return new UserOperationResult { isSuccess = false, message = "Empty UserName" };
            }
            else if (string.IsNullOrEmpty(user.Name))
            {
                return new UserOperationResult { isSuccess = false, message = "Empty Name" };
            }
            else if (string.IsNullOrEmpty(user.eMail)){
                return new UserOperationResult { isSuccess = false, message = "Empty eMail" };
            }
            else if (string.IsNullOrEmpty(user.Password))
            {
                return new UserOperationResult { isSuccess = false, message = "Empty Password" };
            }
            else
            {
                string encryptedPassword = encryptService.Encrypt(user.Password);
                user.Password = encryptedPassword;
                user.CreatedDate = DateTime.Now;
                fileOperation.WriteToFile(user);

                return new UserOperationResult { isSuccess = true, createdDate = DateTime.Now, message = "User Created" };
            }
            

        }

        public UserOperationResult readListUpdate(User user, Book book)
        {
            UserOperationResult userOperationResult = fileOperation.WriteTheReadListToTheFile(user, book);
            return userOperationResult;
        }

        public User userInfo(LoginRequest loginRequest)
        {
            User user = fileOperation.userInfo(loginRequest);
            return user;
        }
    }
}
