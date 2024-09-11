using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Administrator.ViewModels
{
    public class VMSong
    {
        public int Id { get; set; }

        [DisplayName("Song Name")]
        [Required(ErrorMessage = "Song name is required")]
        public string Name { get; set; } = null!;

        [DisplayName("Tempo")]
        [Required(ErrorMessage = "Song tempo is required")]
        public string? Tempo { get; set; }

        [DisplayName("Melody")]
        [Required(ErrorMessage = "Song melody is required")]
        public string? Melody { get; set; }

        [DisplayName("Language")]
        [Required(ErrorMessage = "Song language is required")]
        public string? Language { get; set; }

        [DisplayName("Year of release")]
        [Required(ErrorMessage = "Year of release is required")]
        public int? YearOfRelease { get; set; }

        [DisplayName("Performer")]
        [Required(ErrorMessage = "Performer ID is required")]
        public int PerformerId { get; set; }
    }
}
