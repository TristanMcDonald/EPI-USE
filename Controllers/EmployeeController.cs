using EPI_USE.Data;
using EPI_USE.DataAccess;
using EPI_USE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EPI_USE.Controllers
{
    public class EmployeeController : Controller
    {
        //Creating an object of the database context
        EPI_USE_Context epi_use_context = new EPI_USE_Context();

        //GET: EmployeeDAL
        //Creating an object of the EmployeeDAL
        public EmployeeDAL employeeDAL = new EmployeeDAL();

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult AllEmployees()
        {
            //Get all employees
            var employeeList = new List<Employee>();
            //Calling the DAL method which parses a list with all employees
            employeeList = employeeDAL.GetAllEmployees().ToList();
            //Return the list of employees
            return View(employeeList);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee([Bind] Employee emp)
        {
            if (ModelState.IsValid)
            {
                //Calling the DAL method to create a new employee.
                employeeDAL.CreateEmployee(emp);
                //once the employee is created, redirect to the Index view.
                return RedirectToAction("AllEmployees");
            }
            return View(emp);
        }


        //Get Employee Details
        [Route("Employee/Details/{employeeNo}")]
        [HttpGet]
        public IActionResult Details(string employeeNo)
        {
            if (employeeNo == null)
            {
                return NotFound();
            }

            Employee emp = employeeDAL.GetEmployee(employeeNo);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }


        //Edit Employees
        [Route("Employee/Edit/{employeeNo}")]
        public IActionResult Edit(string employeeNo)
        {
            if (employeeNo == null)
            {
                return NotFound();
            }

            Employee emp = employeeDAL.GetEmployee(employeeNo);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        //Update Employee
        [Route("Employee/Edit/{employeeNo}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string employeeNo, [Bind] Employee emp)
        {
            if (employeeNo == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                employeeDAL.UpdateEmployee(employeeNo, emp);
                return RedirectToAction("AllEmployees");
            }
            return View(employeeDAL);
        }


        //Get Delete View
        [Route("Employee/Delete/{employeeNo}")]
        public IActionResult Delete(string employeeNo)
        {
            if (employeeNo == null)
            {
                return NotFound();
            }
            Employee emp = employeeDAL.GetEmployee(employeeNo);

            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        //Perform Delete
        [Route("Employee/Delete/{employeeNo}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteEmployee(string employeeNo)
        {
            employeeDAL.DeleteEmployee(employeeNo);
            return RedirectToAction("AllEmployees");
        }


    }
}
