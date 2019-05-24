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
    public class WeaponCategoryController : Controller
    {
        // GET: WeaponCategory
        public ActionResult Index()
        {
            return View(DAL.GetWeaponCategories());
        }

        // GET: WeaponCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponCategory WeaponCategory = DAL.GetWeaponCategory((int)id);
            if (WeaponCategory == null)
            {
                return NotFound();
            }

            return View(WeaponCategory);
        }

        // GET: WeaponCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeaponCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID")] WeaponCategory WeaponCategory)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateWeaponCategory(WeaponCategory) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(WeaponCategory);
        }

        // GET: WeaponCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponCategory WeaponCategory = DAL.GetWeaponCategory((int)id);
            if (WeaponCategory == null)
            {
                return NotFound();
            }
            return View(WeaponCategory);
        }

        // POST: WeaponCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] WeaponCategory WeaponCategory)
        {
            if (id != WeaponCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateWeaponCategory(WeaponCategory, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(WeaponCategory);
        }

        // GET: WeaponCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponCategory WeaponCategory = DAL.GetWeaponCategory((int)id);
            if(WeaponCategory == null)
            {
                return NotFound();
            }

            return View(WeaponCategory);
        }

        // POST: WeaponCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteWeaponCategory(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
