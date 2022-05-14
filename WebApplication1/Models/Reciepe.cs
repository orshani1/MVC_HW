using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Reciepe
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string CakeName { get; set; } = String.Empty;
        public string ReciepeLevels { get; set; } = String.Empty ;  
    }
}
