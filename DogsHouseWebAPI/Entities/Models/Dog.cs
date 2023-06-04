using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string Name { get; set; } = String.Empty;

        [StringLength(20)]
        [Required]
        public string Color { get; set; } = String.Empty;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter integer number ( > 0) for Tail Length")]
        public ushort TailLength { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter integer number ( > 0) for Weight")]
        public ushort Weight { get; set; }

        [Timestamp]
        public byte[]? Timestamp { get; set; }
    }
}
