using HireRank.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace HireRank.Application.Services
{
    public class CurrentUserService : ICurrentUserService, IScopedService
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Guid GetCurrentUserId()
        {
            var id = _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            return Guid.Parse(id);
        }
    }
}
