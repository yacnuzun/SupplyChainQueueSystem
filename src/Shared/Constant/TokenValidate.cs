using Shared.Helpers.Security.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Constant
{
    public static class TokenValidate
    {
        public static TokenOptions AccountOptions { get; set; }
        public static TokenOptions CustomerOptions { get; set; }
        public static bool TokenOptionValidate()
        {
            var returnData = AccountOptions.Issuer == CustomerOptions.Issuer;
            var returnAudience = AccountOptions.Audience == CustomerOptions.Audience;
            var returnSecurityKey = AccountOptions.SecurityKey == CustomerOptions.SecurityKey;
            var returnAccessTokenExpiration = AccountOptions.AccessTokenExpiration == CustomerOptions.AccessTokenExpiration;

            return returnData && returnAudience && returnSecurityKey && returnAccessTokenExpiration;
        }
    }
}
