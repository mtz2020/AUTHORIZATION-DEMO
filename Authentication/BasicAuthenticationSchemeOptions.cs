using Microsoft.AspNetCore.Authentication;

namespace AuthorizationDemo.Authentication
{
    public class BasicAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public string UserName { get; set; }
    }
}