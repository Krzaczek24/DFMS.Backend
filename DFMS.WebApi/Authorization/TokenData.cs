using System;

namespace DFMS.WebApi.Authorization
{
    public class TokenData
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
