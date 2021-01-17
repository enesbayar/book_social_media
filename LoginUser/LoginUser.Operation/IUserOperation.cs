using LoginUser.Domain;

namespace LoginUser.Operation
{
    public interface IUserOperation
    {
        UserOperationResult CreateUser(User user);
        UserOperationResult readListUpdate(User user, Book book);
        User userInfo(LoginRequest loginRequest);
    }
}