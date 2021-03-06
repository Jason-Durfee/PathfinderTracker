﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PathfinderTracker.Models;

namespace PathfinderTracker
{
    public class ArmorAddonTypeController : Controller
    {
        // GET: ArmorAddonType
        public ActionResult Index()
        {
            return View(DAL.GetArmorAddonTypes());
        }

        // GET: ArmorAddonType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorAddonType armorAddonType = DAL.GetArmorAddonType((int)id);
            if (armorAddonType == null)
            {
                return NotFound();
            }
            return View(armorAddonType);
        }

        // GET: ArmorAddonType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArmorAddonType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,GPValue,ArmorCheckPenalty,Weight,ID")] ArmorAddonType armorAddonType)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateArmorAddonType(armorAddonType) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(armorAddonType);
        }

        // GET: ArmorAddonType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorAddonType armorAddonType = DAL.GetArmorAddonType((int)id);
            if (armorAddonType == null)
            {
                return NotFound();
            }
            return View(armorAddonType);
        }

        // POST: ArmorAddonType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] ArmorAddonType armorAddonType)
        {
            if (id != armorAddonType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateArmorAddonType(armorAddonType, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(armorAddonType);
        }

        // GET: ArmorAddonType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArmorAddonType armorAddonType = DAL.GetArmorAddonType((int)id);
            if (armorAddonType == null)
            {
                return NotFound();
            }

            return View(armorAddonType);
        }

        // POST: ArmorAddonType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteArmorAddonType(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
