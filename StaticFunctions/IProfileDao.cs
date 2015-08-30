namespace StaticFunctions
{
    public interface IProfileDao
    {
        string GetPassword(string account);

        string GetToken(string account);
    }
}