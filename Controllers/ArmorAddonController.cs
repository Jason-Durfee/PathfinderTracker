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
    public class ArmorAddonController : Controller
    {
        // GET: ArmorAddon
        public ActionResult Index()
        {
            return View(DAL.GetArmorAddons());
        }

        // GET: ArmorAddon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorAddon armorAddon = DAL.GetArmorAddon((int)id);
            if (armorAddon == null)
            {
                return NotFound();
            }

            return View(armorAddon);
        }

        // GET: ArmorAddon/Create
        public ActionResult Create()
        {
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "ID");
            return View();
        }

        // POST: ArmorAddon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("GPValue,ArmorCheckPenalty,Weight,MaterialID,Name,ID")] ArmorAddon armorAddon)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateArmorAddon(armorAddon) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "ID", armorAddon.MaterialID);
            return View(armorAddon);
        }

        // GET: ArmorAddon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorAddon armorAddon = DAL.GetArmorAddon((int)id);
            if (armorAddon == null)
            {
                return NotFound();
            }
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "ID", armorAddon.MaterialID);
            return View(armorAddon);
        }

        // POST: ArmorAddon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("GPValue,ArmorCheckPenalty,Weight,MaterialID,Name,ID")] ArmorAddon armorAddon)
        {
            if (id != armorAddon.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateArmorAddon(armorAddon, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "ID", armorAddon.MaterialID);
            return View(armorAddon);
        }

        // GET: ArmorAddon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorAddon armorAddon = DAL.GetArmorAddon((int)id);
            if (armorAddon == null)
            {
                return NotFound();
            }

            return View(armorAddon);
        }

        // POST: ArmorAddon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteArmorAddon(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
