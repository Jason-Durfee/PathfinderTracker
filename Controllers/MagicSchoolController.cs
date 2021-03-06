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
    public class MagicSchoolController : Controller
    {
        // GET: MagicSchool
        public ActionResult Index()
        {
            return View(DAL.GetMagicSchools());
        }

        // GET: MagicSchool/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MagicSchool magicSchool = DAL.GetMagicSchool((int)id);
            if (magicSchool == null)
            {
                return NotFound();
            }

            return View(magicSchool);
        }

        // GET: MagicSchool/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MagicSchool/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description,Name,ID")] MagicSchool magicSchool)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateMagicSchool(magicSchool) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(magicSchool);
        }

        // GET: MagicSchool/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MagicSchool magicSchool = DAL.GetMagicSchool((int)id);
            if (magicSchool == null)
            {
                return NotFound();
            }
            return View(magicSchool);
        }

        // POST: MagicSchool/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Description,Name,ID")] MagicSchool magicSchool)
        {
            if (id != magicSchool.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateMagicSchool(magicSchool, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(magicSchool);
        }

        // GET: MagicSchool/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MagicSchool magicSchool = DAL.GetMagicSchool((int)id);
            if (magicSchool == null)
            {
                return NotFound();
            }

            return View(magicSchool);
        }

        // POST: MagicSchool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteMagicSchool(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
