using System;

namespace HireRank.Application.Services.Interfaces
{
    public interface ICurrentUserService 
    {
        Guid GetCurrentUserId();
    }
}
