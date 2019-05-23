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
    public class WeaponController : Controller
    {
        // GET: Weapon
        public ActionResult Index()
        {
            return View(DAL.GetWeapons());
        }

        // GET: Weapon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Weapon weapon = DAL.GetWeapon((int)id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // GET: Weapon/Create
        public IActionResult Create()
        {
            ViewData["DamageTypeID"] = new SelectList(DAL.GetDamageTypes(), "ID", "Name");
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "Name");
            ViewData["WeaponSubTypeID"] = new SelectList(DAL.GetWeaponSubTypes(), "ID", "Name");
            ViewData["WeaponTypeID"] = new SelectList(DAL.GetWeaponTypes(), "ID", "Name");
            return View();
        }

        // POST: Weapon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("AttackDiceSmall,AttackDiceMedium,Critical,SpecialAttributes,GPValue,AttackRange,Weight,WeaponSubTypeID,WeaponTypeID,DamageTypeID,MaterialID,Name,ID")] Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateWeapon(weapon) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DamageTypeID"] = new SelectList(DAL.GetDamageTypes(), "ID", "Name", weapon.DamageTypeID);
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "Name", weapon.MaterialID);
            ViewData["WeaponSubTypeID"] = new SelectList(DAL.GetWeaponSubTypes(), "ID", "Name", weapon.WeaponSubTypeID);
            ViewData["WeaponTypeID"] = new SelectList(DAL.GetWeaponTypes(), "ID", "Name", weapon.WeaponTypeID);
            return View(weapon);
        }

        // GET: Weapon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Weapon weapon = DAL.GetWeapon((int)id);
            if(weapon == null)
            {
                return NotFound();
            }
            ViewData["DamageTypeID"] = new SelectList(DAL.GetDamageTypes(), "ID", "Name", weapon.DamageTypeID);
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "Name", weapon.MaterialID);
            ViewData["WeaponSubTypeID"] = new SelectList(DAL.GetWeaponSubTypes(), "ID", "Name", weapon.WeaponSubTypeID);
            ViewData["WeaponTypeID"] = new SelectList(DAL.GetWeaponTypes(), "ID", "Name", weapon.WeaponTypeID);
            return View(weapon);
        }

        // POST: Weapon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("AttackDiceSmall,AttackDiceMedium,Critical,SpecialAttributes,GPValue,AttackRange,Weight,WeaponSubTypeID,WeaponTypeID,DamageTypeID,MaterialID,Name,ID")] Weapon weapon)
        {
            if (id != weapon.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateWeapon(weapon, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DamageTypeID"] = new SelectList(DAL.GetDamageTypes(), "ID", "Name", weapon.DamageTypeID);
            ViewData["MaterialID"] = new SelectList(DAL.GetMaterials(), "ID", "Name", weapon.MaterialID);
            ViewData["WeaponSubTypeID"] = new SelectList(DAL.GetWeaponSubTypes(), "ID", "Name", weapon.WeaponSubTypeID);
            ViewData["WeaponTypeID"] = new SelectList(DAL.GetWeaponTypes(), "ID", "Name", weapon.WeaponTypeID);
            return View(weapon);
        }

        // GET: Weapon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Weapon weapon = DAL.GetWeapon((int)id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // POST: Weapon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteWeapon(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}