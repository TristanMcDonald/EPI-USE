using EPI_USE.DataAccess;
using EPI_USE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EPI_USE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //GET: EmployeeDAL
        //Creating an object of the EmployeeDAL
        public EmployeeDAL employeeDAL = new EmployeeDAL();

        //GET: ManagerDAL
        //Creating an object of the ManagerDAL
        public ManagerDAL managerDAL = new ManagerDAL();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            var managerList = new List<Manager>();
            //Calling the DAL method which parses a list with all managers
            managerList = managerDAL.GetAllManagers().ToList();

            var employeeList = new List<Employee>();
            //Calling the DAL method which parses a list with all employees
            employeeList = employeeDAL.GetAllEmployees().ToList();

            //Creating a new list whereby only the employees with managers are added.
            var filteredEmployeeList = new List<Employee>();

            foreach (var emp in employeeList)
            {
                if (emp.ReportingLineManager != "")
                {
                    filteredEmployeeList.Add(emp);
                }
            }

            //Loop and add the Parent Nodes.
            foreach (Manager man in managerList)
            {
                nodes.Add(new TreeViewNode { id = man.ManagerNumber.ToString(), parent = "#", text = man.Name + " " + man.Surname + " - Manager" });
            }

            //Loop and add the Child Nodes.
            foreach (Employee emp in filteredEmployeeList)
            {
                nodes.Add(new TreeViewNode { id = emp.ReportingLineManager.ToString() + "-" + emp.EmployeeNumber.ToString(), parent = emp.ReportingLineManager.ToString(), text = emp.Name + " " + emp.Surname });
            }

            //Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
