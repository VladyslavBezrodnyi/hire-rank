using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels.Shared;
using HireRank.Common.Configurations;
using HireRank.Common.ExceptionBuilders;
using HireRank.Common.Exceptions;
using HireRank.Common.ModelValidators;
using HireRank.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HireRank.Application.Services
{
    public class UserService : IUserService, IScopedService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthConfiguration _authConfiguration;

        public UserService(UserManager<User> userManager,
                              AuthConfiguration authConfiguration)
        {
            _userManager = userManager;
            _authConfiguration = authConfiguration;
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task CheckIfThePasswordIsValid(string password)
        {
            var passwordValidator = new PasswordValidator<User>();
            var isValid = (await passwordValidator.ValidateAsync(_userManager, null, password)).Succeeded;
            if (!isValid) throw new ValidationException("Invalid password");
        }

        public async Task CheckIfThePasswordIsCorrect(User user, string password)
        {
            bool IsPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            if (!IsPasswordCorrect)
                throw new ValidationException("Wrong password");
        }

        public async Task CheckIfTheUserDoesNotExist(User user)
        {
            User foundUser = await _userManager.FindByEmailAsync(user.Email);
            if (foundUser != null) throw new ValidationException("User with this email already exists");
        }

        public async Task<User> FindUserByEmail(string email)
        {
            User foundUser = await _userManager.FindByEmailAsync(email);
            if (foundUser == null) throw new ValidationException("There is no user with such email");
            return foundUser;
        }

        public async Task RegisterUserAsync<TUser>(TUser user, string password, string role) where TUser : User
        {
            ValidationResults result = ModelValidator.IsValid(user);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            await CheckIfThePasswordIsValid(password);
            await CheckIfTheUserDoesNotExist(user);

            var isCreated = await _userManager.CreateAsync(user, password);
            if (!isCreated.Succeeded)
            {
                throw new DatabaseException("Cann't create new user");
            }
            var isAddedToRole = await _userManager.AddToRoleAsync(user, role);
            if (!isAddedToRole.Succeeded)
            {
                throw new DatabaseException($"Cann't add new user to {role}'s role");
            }
        }

        public TokenResponse GenerateJwtToken(User user, IList<string> roles)
        {
            string stringOfRoles = String.Join(" ", roles.ToArray());
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, stringOfRoles),
            };

            var token = new JwtSecurityToken(
                issuer: _authConfiguration.ISSUER,
                audience: _authConfiguration.AUDIENCE,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_authConfiguration.LIFETIME),
                signingCredentials: new SigningCredentials(
                        _authConfiguration.KEY,
                        SecurityAlgorithms.HmacSha256)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponse() { AccessToken = jwtToken };
        }
    }
}
