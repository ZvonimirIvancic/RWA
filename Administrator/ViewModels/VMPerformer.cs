using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Administrator.ViewModels
{
    public class VMPerformer
    {
        public int Id { get; set; }

        [DisplayName("Performer First Name")]
        [Required(ErrorMessage = "Performer first name is required")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Performer Last Name")]
        [Required(ErrorMessage = "Performer last name is required")]
        public string LastName { get; set; } = null!;

        [DisplayName("Performer Year of Birth")]
        [Required(ErrorMessage = "Performer year of birth is required")]
        public int? YearOfBirth { get; set; }
    }
}
