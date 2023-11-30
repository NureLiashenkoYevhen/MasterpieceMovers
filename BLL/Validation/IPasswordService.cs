namespace BLL.Validation
{
    public interface IPasswordService
    {
        public (string hash, string salt) HashPassword(string passwordToHash);

        public bool IsValid(string inputPassword, string passwordInDb, string salt);
    }
}
