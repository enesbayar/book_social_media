using LoginUser.Domain;

namespace LoginUser.Operation
{
    public interface IUserOperation
    {
        UserOperationResult CreateUser(User user);
    }
}