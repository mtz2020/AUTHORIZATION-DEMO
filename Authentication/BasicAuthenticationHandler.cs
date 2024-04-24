using AuthorizationDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace AuthorizationDemo.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // 1.从Request请求中获取Header信息
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization header is not specified."));
            }
            var authHeader = Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Basic "))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization header value is not in a correct format"));
            }
            var base64EncodedValue = authHeader["Basic ".Length..];
            var userNamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedValue));
            var userName = userNamePassword.Split(':')[0];
            var password = userNamePassword.Split(':')[1];
            var user = UserDTO.AllUsers.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (user == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password."));
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, string.Join(',', user.Roles)),
                new Claim(ClaimTypes.UserData, user.Age.ToString())
            };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Basic", ClaimTypes.NameIdentifier, ClaimTypes.Role));
            var ticket = new AuthenticationTicket(claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = false
            }, "Basic");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
