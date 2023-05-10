using Microsoft.AspNetCore.Identity;

namespace ProniaBackEndProject.Model
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
