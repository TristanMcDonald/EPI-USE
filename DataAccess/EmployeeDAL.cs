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
        public void UpdateEmployee(string employeeNo, Employee employee)
        {
            var fetchedEmployee = from emp in epi_use_context.Employees
                           where emp.EmployeeNumber.Equals(employeeNo)
                           select emp;

            //Creating an object of the Employee class to access the properties and assign relevant values entered
            //by the user to the object properties.
            Employee updatedEmp = new Employee();
            updatedEmp.EmployeeNumber = employeeNo;
            updatedEmp.Name = employee.Name;
            updatedEmp.Surname = employee.Surname;
            updatedEmp.BirthDate = employee.BirthDate;
            updatedEmp.Salary = employee.Salary;
            updatedEmp.Position = employee.Position;
            updatedEmp.ReportingLineManager = employee.ReportingLineManager;

            //Removing the old data for the employee who's data is being updated.
            epi_use_context.Employees.Remove(fetchedEmployee.SingleOrDefault());

            //Adding the updated employee data to the database
            epi_use_context.Employees.Add(updatedEmp);

            //Saving the changes made to the database.
            epi_use_context.SaveChanges();

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
