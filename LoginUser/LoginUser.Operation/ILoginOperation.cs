using LoginUser.Domain;

namespace LoginUser.Operation
{
    public interface ILoginOperation{
        LoginResult Login(LoginRequest loginRequest);
    }
}