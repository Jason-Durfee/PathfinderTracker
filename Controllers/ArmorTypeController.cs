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
    public class ArmorTypeController : Controller
    {
        // GET: ArmorType
        public ActionResult Index()
        {
            return View(DAL.GetArmorTypes());
        }

        // GET: ArmorType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorType armorType = DAL.GetArmorType((int)id);
            if (armorType == null)
            {
                return NotFound();
            }

            return View(armorType);
        }

        // GET: ArmorType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArmorType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID")] ArmorType armorType)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateArmorType(armorType) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(armorType);
        }

        // GET: ArmorType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorType armorType = DAL.GetArmorType((int)id);
            if (armorType == null)
            {
                return NotFound();
            }
            return View(armorType);
        }

        // POST: ArmorType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] ArmorType armorType)
        {
            if (id != armorType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateArmorType(armorType, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(armorType);
        }

        // GET: ArmorType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorType armorType = DAL.GetArmorType((int)id);
            if (armorType == null)
            {
                return NotFound();
            }

            return View(armorType);
        }

        // POST: ArmorType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteArmorType(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
