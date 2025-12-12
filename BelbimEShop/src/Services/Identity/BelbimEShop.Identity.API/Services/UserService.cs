namespace BelbimEShop.Identity.API.Services
{

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
    }
    public class UserService
    {
        public User ValidateUserCredentials(string username, string password)
        {
            // Basit bir doğrulama örneği
            if (username == "test" && password == "password")
            {
                return new User
                {
                    Username = username,
                    Email = "test@example.com",
                    Role = "Admin"
                };
            }

            return null;
        }
    }
}
