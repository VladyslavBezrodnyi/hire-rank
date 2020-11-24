namespace HireRank.Common.Exceptions
{
    public class DatabaseException : HireRankException
    {
        public DatabaseException(string error) : base(error) { }
    }
}
