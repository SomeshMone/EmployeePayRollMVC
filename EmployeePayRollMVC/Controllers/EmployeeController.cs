using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePayRollMVC.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeBusiness _business;

        public EmployeeController(IEmployeeBusiness business)
        {
            this._business=business;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetEmployees")]
        public IActionResult ListOfEmployee()
        {
            List<Employee> employee = new List<Employee>();
            employee = _business.GetAllEmployees().ToList();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.data = "Add Employee";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _business.AddEmployee(employee);
                return RedirectToAction("ListOfEmployee");
            }
            return View(employee);
        }

        [HttpGet]
        //[Route("Update/{id}")]
        public IActionResult Edit(int id)
        {
            //id = (int)HttpContext.Session.GetInt32("Id");

            if (id == null)
            {
                return NotFound();
            }
            Employee employee = _business.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            var res = _business.EmployeeUpdate(employee);
            try
            {
                if (ModelState.IsValid)
                {
                    _business.EmployeeUpdate(employee);
                    return RedirectToAction("ListOfEmployee");
                }
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(employee);
            }
            
        }

        [HttpGet]
        public IActionResult GetEmpById(int id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = _business.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Something went Wrong....");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id ==0)
            {
                return NotFound("Something Wrong");
            }
            Employee employee = _business.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("something went wrong");
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            var result = _business.DeleteEmployee(id);
            if (result)
            {
                return RedirectToAction("ListOfEmployee");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Login(LoginModel model)
        {
            var  res=_business.Login(model);
            if (res == null)
            {
                return Content("Invalid Credintails");
            }
            else
            {
                HttpContext.Session.SetInt32("Id", res.EmployeeId);
                return RedirectToAction("GetEmpById", new { Id = res.EmployeeId });
            }


        }
        [HttpGet]
        public IActionResult GetEmployeeName(string name)
        {
            if (name == null)
            {
                return NotFound();
            }
            Employee emp = _business.GetEmployeeBYName(name);
            if (emp == null)
            {
                return NotFound("something went worng");

            }
            return View(emp);


        }
        [HttpGet]
        public IActionResult Edit1(string name)
        {
            if(name == null)
            {
                return NotFound();
            }
            Employee emp = _business.GetEmployeeBYName(name);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit1(Employee emp)
        {
            var res = _business.InsertUpdate(emp);
            if (res)
            {
                return RedirectToAction("ListOfEmployee");
            }
            return NotFound();
        }

    }
}
