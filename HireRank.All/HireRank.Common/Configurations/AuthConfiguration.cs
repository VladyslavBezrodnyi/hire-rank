using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace HireRank.Common.Configurations
{
    public class AuthConfiguration
    {

        public string ISSUER { get; private set; }
        public string AUDIENCE { get; private set; }
        public SymmetricSecurityKey KEY { get; private set; }
        public int LIFETIME { get; private set; }

        public AuthConfiguration(IConfiguration configuration)
        {
            ISSUER = configuration["Auth:Jwt:Issuer"];
            AUDIENCE = configuration["Auth:Jwt:Issuer"];
            KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Jwt:Key"]));
            LIFETIME = Int32.Parse(configuration["Auth:Jwt:ExpireMinutes"]);
        }
    }
}
