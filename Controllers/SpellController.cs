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
    public class SpellController : Controller
    {
        // GET: Spell
        public ActionResult Index()
        {
            return View(DAL.GetSpells());
        }

        // GET: Spell/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Spell spell = DAL.GetSpell((int)id);
            if (spell == null)
            {
                return NotFound();
            }

            return View(spell);
        }

        // GET: Spell/Create
        public IActionResult Create()
        {
            ViewData["MagicSchoolID"] = new SelectList(DAL.GetMagicSchools(), "ID", "Name");
            return View();
        }

        // POST: Spell/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("MagicSchoolID,Description,CastingTime,RangeDistance,Target,Duration,SavingThrow,SpellResistance,Name,ID")] Spell spell)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateSpell(spell) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MagicSchoolID"] = new SelectList(DAL.GetMagicSchools(), "ID", "Name", spell.MagicSchoolID);
            return View(spell);
        }

        // GET: Spell/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Spell spell = DAL.GetSpell((int)id);
            if(spell == null)
            {
                return NotFound();
            }
            ViewData["MagicSchoolID"] = new SelectList(DAL.GetMagicSchools(), "ID", "Name", spell.MagicSchoolID);
            return View(spell);
        }

        // POST: Spell/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("MagicSchoolID,Description,CastingTime,RangeDistance,Target,Duration,SavingThrow,SpellResistance,Name,ID")] Spell spell)
        {
            if (id != spell.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateSpell(spell, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MagicSchoolID"] = new SelectList(DAL.GetMagicSchools(), "ID", "Name", spell.MagicSchoolID);
            return View(spell);
        }

        // GET: Spell/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Spell spell = DAL.GetSpell((int)id);
            if(spell == null)
            {
                return NotFound();
            }

            return View(spell);
        }

        // POST: Spell/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteSpell(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// searches for a list of spells containing the search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IActionResult Search(string searchText) {
            List<Spell> spells = new List<Spell>();
            List<Spell> allSpells = DAL.GetSpells();
            if(searchText == null || searchText == "") {
                return View("Index", allSpells);
            }
            foreach(Spell spell in allSpells) {
                if(spell.Name.ToLower().Contains(searchText.ToLower())) {
                    spells.Add(spell);
                }
            }
            return View("Index", spells);
        }
    }
}
