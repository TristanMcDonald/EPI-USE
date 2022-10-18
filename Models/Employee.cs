using System;
using System.ComponentModel.DataAnnotations;

namespace EPI_USE.Models
{
    public class Employee
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Key]
        [Required]
        public string EmployeeNumber { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        public string Position { get; set; }
        public string ReportingLineManager { get; set; }
    }
}
