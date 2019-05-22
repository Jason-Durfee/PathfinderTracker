using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PathfinderTracker.Models;

namespace PathfinderTracker
{
    public class SubClassController : Controller
    {
        // GET: SubClass
        public ActionResult Index()
        {
            return View(DAL.GetSubClasses());
        }

        // GET: SubClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubClass subClass = DAL.GetSubClass((int)id);
            if (subClass == null)
            {
                return NotFound();
            }

            return View(subClass);
        }

        // GET: SubClass/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description,Name,ID")] SubClass subClass)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateSubClass(subClass) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subClass);
        }

        // GET: SubClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubClass subClass = DAL.GetSubClass((int)id);
            if(subClass == null)
            {
                return NotFound();
            }
            return View(subClass);
        }

        // POST: SubClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Description,Name,ID")] SubClass subClass)
        {
            if (id != subClass.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateSubClass(subClass, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subClass);
        }

        // GET: SubClass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubClass subClass = DAL.GetSubClass((int)id);
            if(subClass == null)
            {
                return NotFound();
            }

            return View(subClass);
        }

        // POST: SubClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteSubClass(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
