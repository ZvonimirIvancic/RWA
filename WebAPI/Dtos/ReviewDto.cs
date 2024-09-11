using DAL.Models;
using System.Text.Json.Serialization;

namespace WebAPI.Dtos
{
    public class ReviewDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int Rating { get; set; }

        public bool IsFavorite { get; set; }

        public string? Comment { get; set; }

    }
}
