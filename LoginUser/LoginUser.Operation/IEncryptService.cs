namespace LoginUser.Operation
{
    public interface IEncryptService
    {
        string Encrypt(string password);
        string Decrypt(string password,string hashedPassword);
    }
}
