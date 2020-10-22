using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels.Shared;
using HireRank.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HireRank.Application.Services
{
    public interface IUserService
    {
        Task<IList<string>> GetRolesAsync(User user);

        Task CheckIfThePasswordIsValid(string password);

        Task CheckIfThePasswordIsCorrect(User user, string password);

        Task CheckIfTheUserDoesNotExist(User user);

        Task<User> FindUserByEmail(string email);

        TokenResponse GenerateJwtToken(User user, IList<string> roles);

        Task RegisterUserAsync<TUser>(TUser user, string password, string role) where TUser : User;
    }
}
