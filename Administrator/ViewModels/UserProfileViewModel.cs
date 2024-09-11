using System.ComponentModel.DataAnnotations;

namespace Administrator.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, Phone]
        public string Phone { get; set; }
    }
}
