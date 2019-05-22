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
    public class AlignmentController : Controller
    {

        // GET: Alignment
        public ActionResult Index() {
            return View(DAL.GetAlignments());
        }

        // GET: Alignment/Details/5
        public ActionResult Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            Alignment alignment = DAL.GetAlignment((int)id);
            if(alignment == null) {
                return NotFound();
            }

            return View(alignment);
        }

        // GET: Alignment/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Alignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Abbreviation,Description,Name,ID")] Alignment alignment) {
            if(ModelState.IsValid) {
                if(DAL.CreateAlignment(alignment) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alignment);
        }

        // GET: Alignment/Edit/5
        public ActionResult Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            Alignment alignment = DAL.GetAlignment((int)id);
            if(alignment == null) {
                return NotFound();
            }
            return View(alignment);
        }

        // POST: Alignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Abbreviation,Description,Name,ID")] Alignment alignment) {
            if(id != alignment.ID) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                if(DAL.UpdateAlignment(alignment, alignment.ID) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alignment);
        }

        // GET: Alignment/Delete/5
        public ActionResult Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            Alignment alignment = DAL.GetAlignment((int)id);
            if(alignment == null) {
                return NotFound();
            }

            return View(alignment);
        }

        // POST: Alignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Alignment alignment = DAL.GetAlignment((int)id);
            if(DAL.DeleteAlignment(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
