using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthorizationDemo.Authorization
{
    public class SuperAdminAuthorizationHandler : AuthorizationHandler<SuperAdminRequirement, SuperAdminAuthorizationHandler>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SuperAdminRequirement requirement, SuperAdminAuthorizationHandler resource)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role) &&
            (context.User.FindFirstValue(ClaimTypes.Role)
                ?.Split(',')
                ?.Contains("super_admin") ?? false))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
