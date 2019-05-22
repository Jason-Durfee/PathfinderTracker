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
    public class DeityController : Controller
    {
        // GET: Deity
        public ActionResult Index()
        {
            return View(DAL.GetDeities());
        }

        // GET: Deity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deity deity = DAL.GetDeity((int)id);
            if (deity == null)
            {
                return NotFound();
            }

            return View(deity);
        }

        // GET: Deity/Create
        public IActionResult Create()
        {
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "Name");
            return View();
        }

        // POST: Deity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("AlignmentID,Description,Name,ID")] Deity deity)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateDeity(deity) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "Name", deity.AlignmentID);
            return View(deity);
        }

        // GET: Deity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deity deity = DAL.GetDeity((int)id);
            if (deity == null)
            {
                return NotFound();
            }
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "Name", deity.AlignmentID);
            return View(deity);
        }

        // POST: Deity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("AlignmentID,Description,Name,ID")] Deity deity)
        {
            if (id != deity.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateDeity(deity, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "Name", deity.AlignmentID);
            return View(deity);
        }

        // GET: Deity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deity deity = DAL.GetDeity((int)id);
            if (deity == null)
            {
                return NotFound();
            }

            return View(deity);
        }

        // POST: Deity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteDeity(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
