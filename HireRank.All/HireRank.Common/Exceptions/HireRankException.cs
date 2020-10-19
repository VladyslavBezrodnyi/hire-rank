using System;

namespace HireRank.Common.Exceptions
{
    public class HireRankException : Exception
    {
        public HireRankException(string error) : base(error) { }
    }
}
