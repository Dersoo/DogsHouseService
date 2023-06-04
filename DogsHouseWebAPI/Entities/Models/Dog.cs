using System.ComponentModel.DataAnnotations;

namespace DogsHouseWebAPI.Models
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
        public ushort TailLength { get; set; }

        [Required]
        public ushort Weight { get; set; }

        [Timestamp]
        public byte[]? Timestamp { get; set; }
    }
}
