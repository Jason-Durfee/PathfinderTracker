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
    public class CharacterClassController : Controller
    {
        // GET: CharacterClass
        public ActionResult Index()
        {
            return View(DAL.GetCharacterClasses());
        }

        // GET: CharacterClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CharacterClass characterClass = DAL.GetCharacterClass((int)id);
            if (characterClass == null)
            {
                return NotFound();
            }

            return View(characterClass);
        }

        // GET: CharacterClass/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CharacterClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,HasBloodline,HasDomain,HasMagicSchool,ID")] CharacterClass characterClass)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateCharacterClass(characterClass) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(characterClass);
        }

        // GET: CharacterClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CharacterClass characterClass = DAL.GetCharacterClass((int)id);
            if (characterClass == null)
            {
                return NotFound();
            }
            return View(characterClass);
        }

        // POST: CharacterClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,HasBloodline,HasDomain,HasMagicSchool,ID")] CharacterClass characterClass)
        {
            if (id != characterClass.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateCharacterClass(characterClass, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(characterClass);
        }

        // GET: CharacterClass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CharacterClass characterClass = DAL.GetCharacterClass((int)id);
            if (characterClass == null)
            {
                return NotFound();
            }

            return View(characterClass);
        }

        // POST: CharacterClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteCharacterClass(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
