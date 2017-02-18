using EmploeeApp.Context;
using EmploeeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;



namespace EmploeeApp.Controllers
{
    public class EmploeeController : Controller
    {
        private EmploeeContext db = new EmploeeContext();

     

        // GET: Emploee
      
        public ActionResult Index(string sortOrder, int? page)
        {
            

            ViewBag.CurrentSort = sortOrder;
            // sort by name
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //sort by surname
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";

            var employees = from s in db.Employees
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderBy(s => s.Name);
                    break; 
                case "surname_desc":
                    employees = employees.OrderBy(s => s.Surname);
                    break;
                default:
                    employees = employees.OrderBy(s => s.Id);
                    break;
            }
            // emploee number that is shown on page
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));      
        }



        // GET: Emploee/Createhttp://localhost:60603/Properties/
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emploee/Create
        [HttpPost]
        public ActionResult Create(Emploee emploee)
        {         
            try
            {      
                if (ModelState.IsValid)
                {      
                    db.Employees.Add(emploee);
                    // calculating salaryGross without NPD
                    emploee.SalaryGross = emploee.Salary / 0.76m;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(emploee);  
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Emploee emploee = db.Employees.Find(id);
            if (emploee == null)
                return HttpNotFound();
            return View(emploee);
        }
        // GET: Emploee/Edit/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Emploee emploee = db.Employees.Find(id);
            if (emploee == null)
                return HttpNotFound();
            return View(emploee);
        }

        // POST: Emploee/Edit/5
        [HttpPost]
        public ActionResult Edit(Emploee emploee)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    // recalculating salaryGross without NPD
                    emploee.SalaryGross = emploee.Salary / 0.76m;

                    db.Entry(emploee).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(emploee);
            }
            catch
            {
                return View();
            }
        }

        // GET: Emploee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Emploee emploee = db.Employees.Find(id);
            if (emploee == null)
                   return HttpNotFound();
                return View(emploee);
        }

        // POST: Emploee/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Emploee emploee)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    emploee = db.Employees.Find(id);
                    if (emploee == null)
                        return HttpNotFound();
                    db.Employees.Remove(emploee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(emploee);
            }
            catch
            {
                return View();
            }
        }
    
    }
}
