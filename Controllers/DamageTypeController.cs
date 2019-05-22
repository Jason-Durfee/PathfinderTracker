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
    public class DamageTypeController : Controller
    {
        // GET: DamageType
        public ActionResult Index()
        {
            return View(DAL.GetDamageTypes());
        }

        // GET: DamageType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DamageType damageType = DAL.GetDamageType((int)id);
            if (damageType == null)
            {
                return NotFound();
            }

            return View(damageType);
        }

        // GET: DamageType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DamageType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID")] DamageType damageType)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateDamageType(damageType) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(damageType);
        }

        // GET: DamageType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DamageType damageType = DAL.GetDamageType((int)id);
            if (damageType == null)
            {
                return NotFound();
            }
            return View(damageType);
        }

        // POST: DamageType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] DamageType damageType)
        {
            if (id != damageType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateDamageType(damageType, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(damageType);
        }

        // GET: DamageType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DamageType damageType = DAL.GetDamageType((int)id);
            if (damageType == null)
            {
                return NotFound();
            }

            return View(damageType);
        }

        // POST: DamageType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteDamageType(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
