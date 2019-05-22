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
    public class ClassesToCharacterController : Controller
    {
        // GET: ClassesToCharacter
        public ActionResult Index()
        {
            return View(DAL.GetClassesToCharacters());
        }

        // GET: ClassesToCharacter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClassesToCharacter classesToCharacter = DAL.GetClassesToCharacter((int)id);
            if (classesToCharacter == null)
            {
                return NotFound();
            }

            return View(classesToCharacter);
        }

        // GET: ClassesToCharacter/Create
        public IActionResult Create()
        {
            ViewData["CharacterID"] = new SelectList(DAL.GetCharacters(), "ID", "Name");
            ViewData["ClassID"] = new SelectList(DAL.GetClasses(), "ID", "Name");
            ViewData["SubClassID"] = new SelectList(DAL.GetSubClasses(), "ID", "Name");
            return View();
        }

        // POST: ClassesToCharacter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ClassLevel,CharacterID,ClassID,SubClassID,ID")] ClassesToCharacter classesToCharacter)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateClassesToCharacter(classesToCharacter) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterID"] = new SelectList(DAL.GetCharacters(), "ID", "Name", classesToCharacter.CharacterID);
            ViewData["ClassID"] = new SelectList(DAL.GetClasses(), "ID", "Name", classesToCharacter.ClassID);
            ViewData["SubClassID"] = new SelectList(DAL.GetSubClasses(), "ID", "Name", classesToCharacter.SubClassID);
            return View(classesToCharacter);
        }

        // GET: ClassesToCharacter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClassesToCharacter classesToCharacter = DAL.GetClassesToCharacter((int)id);
            if (classesToCharacter == null)
            {
                return NotFound();
            }
            ViewData["CharacterID"] = new SelectList(DAL.GetCharacters(), "ID", "Name", classesToCharacter.CharacterID);
            ViewData["ClassID"] = new SelectList(DAL.GetClasses(), "ID", "Name", classesToCharacter.ClassID);
            ViewData["SubClassID"] = new SelectList(DAL.GetSubClasses(), "ID", "Name", classesToCharacter.SubClassID);
            return View(classesToCharacter);
        }

        // POST: ClassesToCharacter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ClassLevel,CharacterID,ClassID,SubClassID,ID")] ClassesToCharacter classesToCharacter)
        {
            if (id != classesToCharacter.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateClassesToCharacter(classesToCharacter, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterID"] = new SelectList(DAL.GetCharacters(), "ID", "Name", classesToCharacter.CharacterID);
            ViewData["ClassID"] = new SelectList(DAL.GetClasses(), "ID", "Name", classesToCharacter.ClassID);
            ViewData["SubClassID"] = new SelectList(DAL.GetSubClasses(), "ID", "Name", classesToCharacter.SubClassID);
            return View(classesToCharacter);
        }

        // GET: ClassesToCharacter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClassesToCharacter classesToCharacter = DAL.GetClassesToCharacter((int)id);
            if (classesToCharacter == null)
            {
                return NotFound();
            }

            return View(classesToCharacter);
        }

        // POST: ClassesToCharacter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteClassesToCharacter(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
