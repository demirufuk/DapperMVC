using Dapper;
using DapperMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View(DapperORM.ReturnList<Employee>("EmployeeViewAll"));
        }

        //
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", id);
                return View(DapperORM.ReturnList<Employee>("EmployeeViewById", param).FirstOrDefault<Employee>());
            }

        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee model)
        {
            DynamicParameters param = new DynamicParameters();
                param.Add("Id", model.Id);
                param.Add("Name", model.Name);
                param.Add("Position", model.Position);
                param.Add("Age", model.Age);
                param.Add("Salary", model.Salary);

            DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("Id", id);
            DapperORM.ExecuteWithoutReturn("EmployeeDeleteById", param);

            return RedirectToAction("Index");
        }
    }
}