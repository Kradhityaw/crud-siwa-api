using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleCRUD.DTOs
{
    public class AsalSekolahDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
