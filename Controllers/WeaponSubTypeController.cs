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
    public class WeaponSubTypeController : Controller
    {
        // GET: WeaponSubType
        public ActionResult Index()
        {
            return View(DAL.GetWeaponSubTypes());
        }

        // GET: WeaponSubType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponSubType weaponSubType = DAL.GetWeaponSubType((int)id);
            if (weaponSubType == null)
            {
                return NotFound();
            }

            return View(weaponSubType);
        }

        // GET: WeaponSubType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeaponSubType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID")] WeaponSubType weaponSubType)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateWeaponSubType(weaponSubType) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(weaponSubType);
        }

        // GET: WeaponSubType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponSubType weaponSubType = DAL.GetWeaponSubType((int)id);
            if (weaponSubType == null)
            {
                return NotFound();
            }
            return View(weaponSubType);
        }

        // POST: WeaponSubType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] WeaponSubType weaponSubType)
        {
            if (id != weaponSubType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateWeaponSubType(weaponSubType, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(weaponSubType);
        }

        // GET: WeaponSubType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponSubType weaponSubType = DAL.GetWeaponSubType((int)id);
            if(weaponSubType == null)
            {
                return NotFound();
            }

            return View(weaponSubType);
        }

        // POST: WeaponSubType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteWeaponSubType(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
