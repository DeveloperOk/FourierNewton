using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourierNewton.Common.Auth
{
    public class AuthConstants
    {

        public static readonly string AuthScheme = "FourierNewtonCookieAuth";
        public static readonly string CookieName = "FourierNewtonCookie";
        public static readonly string LoginPath = @"/Sign/SignIn";

        public static readonly string FourierNewtonIdentity = "FourierNewton Identity";
    }
}
