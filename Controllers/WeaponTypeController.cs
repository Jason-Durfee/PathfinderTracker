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
    public class WeaponTypeController : Controller
    {
        // GET: WeaponType
        public ActionResult Index()
        {
            return View(DAL.GetWeaponTypes());
        }

        // GET: WeaponType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponType weaponType = DAL.GetWeaponType((int)id);
            if (weaponType == null)
            {
                return NotFound();
            }

            return View(weaponType);
        }

        // GET: WeaponType/Create
        public IActionResult Create()
        {
            ViewData["WeaponCategoryID"] = new SelectList(DAL.GetWeaponCategories(), "ID", "Name");
            ViewData["CoreTypeID"] = new SelectList(DAL.GetCoreTypes(), "ID", "Name");
            return View();
        }

        // POST: WeaponType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,AttackDiceSmall,AttackDiceMedium,AttackRange,Critical,GPValue,Weight,ID")] WeaponType weaponType)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateWeaponType(weaponType) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["WeaponCategoryID"] = new SelectList(DAL.GetWeaponCategories(), "ID", "Name", weaponType.WeaponCategoryID);
            ViewData["CoreTypeID"] = new SelectList(DAL.GetCoreTypes(), "ID", "Name", weaponType.CoreTypeID);
            return View(weaponType);
        }

        // GET: WeaponType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponType weaponType = DAL.GetWeaponType((int)id);
            if (weaponType == null)
            {
                return NotFound();
            }
            return View(weaponType);
        }

        // POST: WeaponType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,AttackDiceSmall,AttackDiceMedium,AttackRange,Critical,GPValue,Weight,ID")] WeaponType weaponType)
        {
            if (id != weaponType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateWeaponType(weaponType, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["WeaponCategoryID"] = new SelectList(DAL.GetWeaponCategories(), "ID", "Name", weaponType.WeaponCategoryID);
            ViewData["CoreTypeID"] = new SelectList(DAL.GetCoreTypes(), "ID", "Name", weaponType.CoreTypeID);
            return View(weaponType);
        }

        // GET: WeaponType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeaponType weaponType = DAL.GetWeaponType((int)id);
            if(weaponType == null)
            {
                return NotFound();
            }

            return View(weaponType);
        }

        // POST: WeaponType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteWeaponType(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
