using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleCRUD.DTOs
{
    public class SiswaDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [RegularExpression("L|P", ErrorMessage = "Jenis kelamin harus (L) laki - laki, atau (P) perempuan!")]
        [DefaultValue("L | P")]
        [Required]
        public string Sex { get; set; }

        [Required]
        public int AsalSekolahId { get; set; }
    }
}
