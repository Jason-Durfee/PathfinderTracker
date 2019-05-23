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
    public class SlotController : Controller
    {
        // GET: Slot
        public ActionResult Index()
        {
            return View(DAL.GetSlots());
        }

        // GET: Slot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Slot slot = DAL.GetSlot((int)id);
            if (slot == null)
            {
                return NotFound();
            }

            return View(slot);
        }

        // GET: Slot/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Slot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID")] Slot slot)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateSlot(slot) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(slot);
        }

        // GET: Slot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Slot slot = DAL.GetSlot((int)id);
            if(slot == null)
            {
                return NotFound();
            }
            return View(slot);
        }

        // POST: Slot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] Slot slot)
        {
            if (id != slot.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateSlot(slot, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(slot);
        }

        // GET: Slot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Slot slot = DAL.GetSlot((int)id);
            if(slot == null)
            {
                return NotFound();
            }

            return View(slot);
        }

        // POST: Slot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteSlot(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
