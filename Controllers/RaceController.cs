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
    public class RaceController : Controller
    {
        // GET: Race
        public ActionResult Index()
        {
            return View(DAL.GetRaces());
        }

        // GET: Race/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Race race = DAL.GetRace((int)id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // GET: Race/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Race/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID")] Race race)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateRace(race) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(race);
        }

        // GET: Race/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Race race = DAL.GetRace((int)id);
            if (race == null)
            {
                return NotFound();
            }
            return View(race);
        }

        // POST: Race/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] Race race)
        {
            if (id != race.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateRace(race, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(race);
        }

        // GET: Race/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Race race = DAL.GetRace((int)id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // POST: Race/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteRace(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
