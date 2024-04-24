using Microsoft.AspNetCore.Authorization;

namespace AuthorizationDemo.Authorization
{
    /// <summary>
    /// 管理员的授权需求，都有那些需求
    /// </summary>
    public class SuperAdminRequirement : IAuthorizationRequirement
    {
        
    }
}
