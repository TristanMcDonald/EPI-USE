using EPI_USE.Data;
using EPI_USE.Models;
using System.Collections.Generic;
using System.Linq;

namespace EPI_USE.DataAccess
{
    public class EmployeeDAL
    {
        private EPI_USE_Context epi_use_context;
        public EmployeeDAL()
        {
            epi_use_context = new EPI_USE_Context();
        }

        //Method to Create a new employee by adding their information entered to the database (context).
        public void CreateEmployee(Employee employee)
        {
            //Adding the new Employee to the database employees.
            epi_use_context.Employees.Add(employee);
            //Saving the changes made to the database context.
            epi_use_context.SaveChanges();
        }

        //Method to fetch all Employees.
        public IEnumerable<Employee> GetAllEmployees()
        {
            //initializing the Employees list.
            List<Employee> allEmployees = new List<Employee>();

            foreach (var employee in epi_use_context.Employees)
            {
                allEmployees = new List<Employee>
                    (from emp in epi_use_context.Employees
                     select emp
                    );
            }
            return allEmployees;
        }

        //Method to get a specific employee
        public Employee GetEmployee(string employeeNumber)
        {
            //Getting the employee that has the parsed employee number.
            Employee chosenEmployee = new Employee();

            var employee = from emp in epi_use_context.Employees
                           where emp.EmployeeNumber.Equals(employeeNumber)
                           select emp;

            chosenEmployee = employee.SingleOrDefault();

            return chosenEmployee;
        }

        //Method to update an employees information
        public void UpdateEmployee()
        {

        }

        //Method to remove an employee from the database
        public void DeleteEmployee(string employeeNo)
        {
            //Getting the employee that has the parsed employee number.
            Employee chosenEmployee = new Employee();

            var employee = from emp in epi_use_context.Employees
                           where emp.EmployeeNumber.Equals(employeeNo)
                           select emp;

            chosenEmployee = employee.SingleOrDefault();

            //Removing the specified Employee from the database employees.
            epi_use_context.Employees.Remove(chosenEmployee);
            //Saving the changes made to the database context.
            epi_use_context.SaveChanges();
        }

    }
}
