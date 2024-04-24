namespace AuthorizationDemo.Models
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public int Age { get; set; }

        public override string ToString() => UserName;

        public static readonly UserDTO[] AllUsers = {
        new UserDTO
        {
            UserName = "daxnet", Password = "password", Age = 16, Roles = new[] { "admin", "super_admin" }
        },
        new UserDTO
        {
            UserName = "admin", Password = "admin", Age = 29, Roles = new[] { "admin" }
        }
    };
    }
}
