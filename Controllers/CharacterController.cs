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
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "Name");
            ViewData["CampaignID"] = new SelectList(DAL.GetCampaigns(), "ID", "Name");
            ViewData["DeityID"] = new SelectList(DAL.GetDeities(), "ID", "Name");
            ViewData["RaceID"] = new SelectList(DAL.GetRaces(), "ID", "Name");
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
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "Name", character.AlignmentID);
            ViewData["CampaignID"] = new SelectList(DAL.GetCampaigns(), "ID", "Name", character.CampaignID);
            ViewData["DeityID"] = new SelectList(DAL.GetDeities(), "ID", "Name", character.DeityID);
            ViewData["RaceID"] = new SelectList(DAL.GetRaces(), "ID", "Name", character.RaceID);
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
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "Name", character.AlignmentID);
            ViewData["CampaignID"] = new SelectList(DAL.GetCampaigns(), "ID", "Name", character.CampaignID);
            ViewData["DeityID"] = new SelectList(DAL.GetDeities(), "ID", "Name", character.DeityID);
            ViewData["RaceID"] = new SelectList(DAL.GetRaces(), "ID", "Name", character.RaceID);
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
            ViewData["AlignmentID"] = new SelectList(DAL.GetAlignments(), "ID", "Name", character.AlignmentID);
            ViewData["CampaignID"] = new SelectList(DAL.GetCampaigns(), "ID", "Name", character.CampaignID);
            ViewData["DeityID"] = new SelectList(DAL.GetDeities(), "ID", "Name", character.DeityID);
            ViewData["RaceID"] = new SelectList(DAL.GetRaces(), "ID", "Name", character.RaceID);
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

        /// <summary>
        /// searches for a list of Characters containing the search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IActionResult Search(string searchText) {
            List<Character> characters = new List<Character>();
            List<Character> allCharacters = DAL.GetCharacters();
            if(searchText == null || searchText == "") {
                return View("Index", allCharacters);
            }
            foreach(Character character in allCharacters) {
                if(character.Name.ToLower().Contains(searchText.ToLower())) {
                    characters.Add(character);
                }
                else if(character.Name.ToLower().Contains(searchText.ToLower())) {
                    characters.Add(character);
                }
                else if(character.Race != null && character.Race.Name.ToLower().Contains(searchText.ToLower())) {
                    characters.Add(character);
                }
                else if(character.Alignment != null && character.Alignment.Name.ToLower().Contains(searchText.ToLower())) {
                    characters.Add(character);
                }
                else if(character.Deity != null && character.Deity.Name.ToLower().Contains(searchText.ToLower())) {
                    characters.Add(character);
                }
            }
            return View("Index", characters);
        }
    }
}
