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
    public class ArmorController : Controller
    {
        // GET: Armor
        public ActionResult Index()
        {
            return View(DAL.GetArmors());
        }

        // GET: Armor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Armor armor = DAL.GetArmor((int)id);
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // GET: Armor/Create
        public ActionResult Create()
        {
            ViewData["ArmorAddonID"] = new SelectList(DAL.GetArmorAddons(), "ID", "ID");
            ViewData["ArmorTypeID"] = new SelectList(DAL.GetArmorTypes(), "ID", "ID");
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "ID");
            return View();
        }

        // POST: Armor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("GPValue,ACBonus,ArmorCheckPenalty,Weight,MaterialID,ArmorTypeID,ArmorAddonID,SpecialAttributes,Name,ID")] Armor armor)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateArmor(armor) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorAddonID"] = new SelectList(DAL.GetArmorAddons(), "ID", "ID", armor.ArmorAddonID);
            ViewData["ArmorTypeID"] = new SelectList(DAL.GetArmorTypes(), "ID", "ID", armor.ArmorTypeID);
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "ID", armor.MaterialID);
            return View(armor);
        }

        // GET: Armor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Armor armor = DAL.GetArmor((int)id);
            if (armor == null)
            {
                return NotFound();
            }
            ViewData["ArmorAddonID"] = new SelectList(DAL.GetArmorAddons(), "ID", "ID", armor.ArmorAddonID);
            ViewData["ArmorTypeID"] = new SelectList(DAL.GetArmorTypes(), "ID", "ID", armor.ArmorTypeID);
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "ID", armor.MaterialID);
            return View(armor);
        }

        // POST: Armor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("GPValue,ACBonus,ArmorCheckPenalty,Weight,MaterialID,ArmorTypeID,ArmorAddonID,SpecialAttributes,Name,ID")] Armor armor)
        {
            if (id != armor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateArmor(armor, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorAddonID"] = new SelectList(DAL.GetArmorAddons(), "ID", "ID", armor.ArmorAddonID);
            ViewData["ArmorTypeID"] = new SelectList(DAL.GetArmorTypes(), "ID", "ID", armor.ArmorTypeID);
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "ID", armor.MaterialID);
            return View(armor);
        }

        // GET: Armor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Armor armor = DAL.GetArmor((int)id);
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // POST: Armor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Armor armor = DAL.GetArmor(id);
            if(DAL.DeleteArmor(id) > 0) {
                //success
            }
            else {
                //error b
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
