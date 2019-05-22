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
    public class CharacterController : Controller
    {
        // GET: Character
        public ActionResult Index()
        {
            return View(DAL.GetCharacters());
        }

        // GET: Character/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Character character = DAL.GetCharacter((int)id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Character/Create
        public IActionResult Create()
        {
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "ID");
            ViewData["CampaignID"] = new SelectList(DAL.GetCampaigns(), "ID", "ID");
            ViewData["DeityID"] = new SelectList(DAL.GetDeities(), "ID", "ID");
            ViewData["RaceID"] = new SelectList(DAL.GetRaces(), "ID", "ID");
            return View();
        }

        // POST: Character/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Level,IsNPC,AlignmentID,DeityID,RaceID,CampaignID,Name,ID")] Character character)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateCharacter(character) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "ID", character.AlignmentID);
            ViewData["CampaignID"] = new SelectList(DAL.GetCampaigns(), "ID", "ID", character.CampaignID);
            ViewData["DeityID"] = new SelectList(DAL.GetDeities(), "ID", "ID", character.DeityID);
            ViewData["RaceID"] = new SelectList(DAL.GetRaces(), "ID", "ID", character.RaceID);
            return View(character);
        }

        // GET: Character/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Character character = DAL.GetCharacter((int)id);
            if (character == null)
            {
                return NotFound();
            }
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "ID", character.AlignmentID);
            ViewData["CampaignID"] = new SelectList(DAL.GetCampaigns(), "ID", "ID", character.CampaignID);
            ViewData["DeityID"] = new SelectList(DAL.GetDeities(), "ID", "ID", character.DeityID);
            ViewData["RaceID"] = new SelectList(DAL.GetRaces(), "ID", "ID", character.RaceID);
            return View(character);
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Level,IsNPC,AlignmentID,DeityID,RaceID,CampaignID,Name,ID")] Character character)
        {
            if (id != character.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateCharacter(character, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "ID", character.AlignmentID);
            ViewData["CampaignID"] = new SelectList(DAL.GetCampaigns(), "ID", "ID", character.CampaignID);
            ViewData["DeityID"] = new SelectList(DAL.GetDeities(), "ID", "ID", character.DeityID);
            ViewData["RaceID"] = new SelectList(DAL.GetRaces(), "ID", "ID", character.RaceID);
            return View(character);
        }

        // GET: Character/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Character character = DAL.GetCharacter((int)id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteCharacter(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
