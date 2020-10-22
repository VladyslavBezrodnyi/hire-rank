namespace HireRank.Common.Exceptions
{
    public class ValidationException : HireRankException
    {
        public ValidationException(params string[] errorDescriptions) : base (string.Join(", ", errorDescriptions)) { } 
    }
}
