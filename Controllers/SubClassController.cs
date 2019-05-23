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
    public class BloodlineController : Controller
    {
        // GET: Bloodline
        public ActionResult Index()
        {
            return View(DAL.GetBloodlines());
        }

        // GET: Bloodline/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bloodline Bloodline = DAL.GetBloodline((int)id);
            if (Bloodline == null)
            {
                return NotFound();
            }

            return View(Bloodline);
        }

        // GET: Bloodline/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bloodline/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description,Name,ID")] Bloodline Bloodline)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateBloodline(Bloodline) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Bloodline);
        }

        // GET: Bloodline/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bloodline Bloodline = DAL.GetBloodline((int)id);
            if(Bloodline == null)
            {
                return NotFound();
            }
            return View(Bloodline);
        }

        // POST: Bloodline/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Description,Name,ID")] Bloodline Bloodline)
        {
            if (id != Bloodline.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateBloodline(Bloodline, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Bloodline);
        }

        // GET: Bloodline/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bloodline Bloodline = DAL.GetBloodline((int)id);
            if(Bloodline == null)
            {
                return NotFound();
            }

            return View(Bloodline);
        }

        // POST: Bloodline/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteBloodline(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
