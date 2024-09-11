using System.Text.Json.Serialization;

namespace WebAPI.Dtos
{
    public class PerformerDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int? YearOfBirth { get; set; }

    }
}
