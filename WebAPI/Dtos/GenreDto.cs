using DAL.Models;
using System.Text.Json.Serialization;

namespace WebAPI.Dtos
{
    public class GenreDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int? DebutYear { get; set; }

    }
}
