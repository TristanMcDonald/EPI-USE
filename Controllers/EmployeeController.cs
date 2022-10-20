using EPI_USE.DataAccess;
using EPI_USE.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPI_USE.Controllers
{
    public class EmployeeController : Controller
    {
        //GET: EmployeeDAL
        //Creating an object of the EmployeeDAL
        public EmployeeDAL employeeDAL = new EmployeeDAL();

        public ViewResult ManagerNumberError()
        {
            return View();
        }

        //Get all employees
        //The filter by employee data is implemented within this controller.
        public ViewResult AllEmployees(string searchString)
        {            
            var employeeList = new List<Employee>();
            //Calling the DAL method which parses a list with all employees
            employeeList = employeeDAL.GetAllEmployees().ToList();

            //Declaration for filter.
            ViewData["CurrentFilter"] = searchString;

            //If the searchString contains a value 
            if (!String.IsNullOrEmpty(searchString))
            {
                employeeList = employeeList.Where(e => e.EmployeeNumber.Contains(searchString) ||
                e.Salary.Contains(searchString)|| e.Position.Contains(searchString) || e.BirthDate.Equals(searchString)||
                e.Surname.Contains(searchString) || e.Name.Contains(searchString)).ToList();
            }

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
                try
                {
                    //Calling the DAL method to create a new employee.
                    employeeDAL.CreateEmployee(emp);
                }
                catch (Exception e) 
                {
                    return RedirectToAction("ManagerNumberError");
                }
                
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
                try
                {
                    employeeDAL.UpdateEmployee(emp);
                }
                catch(Exception e)
                {
                    return RedirectToAction("ManagerNumberError");
                }
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
