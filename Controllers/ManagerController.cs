using EPI_USE.DataAccess;
using EPI_USE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EPI_USE.Controllers
{
    public class ManagerController : Controller
    {
        //GET: ManagerDAL
        //Creating an object of the ManagerDAL
        public ManagerDAL managerDAL = new ManagerDAL();

        public IActionResult Index()
        {
            return View();
        }

        //Get all managers
        public ViewResult AllManagers()
        {
            var managerList = new List<Manager>();
            //Calling the DAL method which parses a list with all managers
            managerList = managerDAL.GetAllManagers().ToList();

            //Return the list of managers
            return View(managerList);
        }

        [HttpGet]
        public IActionResult CreateManager()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateManager([Bind] Manager man)
        {
            if (ModelState.IsValid)
            {
                //Calling the DAL method to create a new manager.
                managerDAL.CreateManager(man);
                //once the manager is created, redirect to the AllManagers view.
                return RedirectToAction("Allmanagers");
            }
            return View(man);
        }


        //Get Manager Details
        [Route("Manager/Details/{managerNo}")]
        [HttpGet]
        public IActionResult Details(string managerNo)
        {
            if (managerNo == null)
            {
                return NotFound();
            }

            Manager man = managerDAL.GetManager(managerNo);

            if (man == null)
            {
                return NotFound();
            }

            return View(man);
        }


        //Edit Manager
        [Route("Manager/Edit/{managerNo}")]
        public IActionResult Edit(string managerNo)
        {
            if (managerNo == null)
            {
                return NotFound();
            }

            Manager man = managerDAL.GetManager(managerNo);

            if (man == null)
            {
                return NotFound();
            }

            return View(man);
        }

        //Update Manager
        [Route("Manager/Edit/{managerNo}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string managerNo, [Bind] Manager man)
        {
            if (managerNo == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                managerDAL.UpdateManager(man);
                return RedirectToAction("AllManagers");
            }
            return View(managerDAL);
        }


        //Get Delete View
        [Route("Manager/Delete/{managerNo}")]
        public IActionResult Delete(string managerNo)
        {
            if (managerNo == null)
            {
                return NotFound();
            }
            Manager man = managerDAL.GetManager(managerNo);

            if (man == null)
            {
                return NotFound();
            }
            return View(man);
        }

        //Perform Delete
        [Route("Manager/Delete/{managerNo}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteManager(string managerNo)
        {
            managerDAL.DeleteManager(managerNo);
            return RedirectToAction("AllManagers");
        }
    }
}
