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
    public class FeatTypeController : Controller
    {
        // GET: FeatTypes
        public ActionResult Index()
        {
            return View(DAL.GetFeatTypes());
        }

        // GET: FeatTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FeatType featType = DAL.GetFeatType((int)id);
            if (featType == null)
            {
                return NotFound();
            }

            return View(featType);
        }

        // GET: FeatTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FeatTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID")] FeatType featType)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateFeatType(featType) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(featType);
        }

        // GET: FeatTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FeatType featType = DAL.GetFeatType((int)id);
            if(featType == null)
            {
                return NotFound();
            }
            return View(featType);
        }

        // POST: FeatTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] FeatType featType)
        {
            if (id != featType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateFeatType(featType, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(featType);
        }

        // GET: FeatTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FeatType featType = DAL.GetFeatType((int)id);
            if(featType == null)
            {
                return NotFound();
            }

            return View(featType);
        }

        // POST: FeatTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteFeatType(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
