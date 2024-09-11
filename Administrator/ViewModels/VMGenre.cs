using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Administrator.ViewModels
{
    public class VMGenre
    {
        public int Id { get; set; }

        [DisplayName("Genre Name")]
        [Required(ErrorMessage = "Genre name is required")]
        public string Name { get; set; } = null!;

        [DisplayName("Genre Description")]
        [Required(ErrorMessage = "Genre description is required")]
        public string? Description { get; set; }

        [DisplayName("Genre Debut Year")]
        [Required(ErrorMessage = "Genre debut year is required")]
        public int? DebutYear { get; set; }
    }
}
