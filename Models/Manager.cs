using System.ComponentModel.DataAnnotations;

namespace EPI_USE.Models
{
    public class Manager
    {
        [Key]
        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Manager Number must be 5 characters long")]
        public string ManagerNumber { get; set; }

        [Required(ErrorMessage = "Please enter a name for this manager")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a surname for this manager")]
        public string Surname { get; set; }
    }
}
