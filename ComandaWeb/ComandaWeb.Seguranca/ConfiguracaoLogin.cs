using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComandaWeb.Seguranca
{
    public class ConfiguracaoLogin
    {
        private readonly string secret = "mysupersecret_secretkey!123";
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public ConfiguracaoLogin()
        {
            var keyByteArray = Encoding.ASCII.GetBytes(secret);
            Key = new SymmetricSecurityKey(keyByteArray);
            SigningCredentials = new SigningCredentials(
                Key,
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
