using System.Collections.Generic;

namespace ttpMiddleware.Configuration
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }

    public class ConfirmEmail
    {
        public string code{ get; set; }
        public string userId{ get; set; }
    }
}