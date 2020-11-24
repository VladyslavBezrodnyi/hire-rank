using System;
using HireRank.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HireRank.Core.Entities
{
    public class User : IdentityUser<Guid>, IEntity
    {
    }
}
