using Microsoft.AspNetCore.Identity;

namespace AuthDatabase.Entities
{
    /// <summary>
    ///  Nazwy Tabel 
    /// </summary>

    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
