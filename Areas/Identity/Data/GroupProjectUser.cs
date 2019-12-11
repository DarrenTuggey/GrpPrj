using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GroupProject.Areas.Identity.Data
{
    // Adds first and last name user properties and inherit the rest from IdentityUser
    public class GroupProjectUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
    }
}
