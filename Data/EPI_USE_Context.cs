using EPI_USE.Models;
using System.Data.Entity;

namespace EPI_USE.Data
{
    public class EPI_USE_Context : DbContext
    {
        public EPI_USE_Context()
        {

        }

        public DbSet<Employee> Employees {get; set;}

    }
}
