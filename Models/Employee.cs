using System;
using System.ComponentModel.DataAnnotations;

namespace EPI_USE.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "Please enter a name for this employee")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a surname for this employee")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter a birth date for this employee")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Key]
        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Employee Number must be 5 characters long")]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Please enter a salary for this employee")]
        public string Salary { get; set; }

        [Required(ErrorMessage = "Please enter a position for this employee")]
        public string Position { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "Manager Number must be 5 characters long")]
        public string ReportingLineManager { get; set; }
    }
}
