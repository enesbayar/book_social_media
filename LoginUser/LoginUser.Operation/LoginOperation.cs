using LoginUser.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginUser.Operation
{
    public class LoginOperation : ILoginOperation
    {
        private readonly IEncryptService encryptService;
        private readonly IFileOperation fileOperation;

        public LoginOperation(IEncryptService encryptService, IFileOperation fileOperation){
            this.encryptService = encryptService;
            this.fileOperation = fileOperation;
        }
        public LoginResult Login(LoginRequest loginRequest){

            User user = fileOperation.ReadFromFile(loginRequest.userName);
            String decryptedPassword = encryptService.Decrypt(loginRequest.password, user.Password);
            if("Password correct" == decryptedPassword){

                return new LoginResult { isSuccess = true , message = "Welcome" };
            }
            if(user.UserName == null)
            {
                return new LoginResult { isSuccess = false, message = "User Not Found" };
            }
            return new LoginResult { isSuccess = false , message = "Wrong Password" };
        }
    }
}
