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
    public class PlayerController : Controller
    {
        // GET: Player
        public ActionResult Index()
        {
            return View(DAL.GetPlayers());
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player player = DAL.GetPlayer((int)id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            ViewData["CharacterID"] = new SelectList(DAL.GetCharacters(), "ID", "Name");
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("HPMax,HPCurrent,CharacterID,Bonuses,Name,ID")] Player player)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreatePlayer(player) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterID"] = new SelectList(DAL.GetCharacters(), "ID", "Name", player.CharacterID);
            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player player = DAL.GetPlayer((int)id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["CharacterID"] = new SelectList(DAL.GetCharacters(), "ID", "Name", player.CharacterID);
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("HPMax,HPCurrent,CharacterID,Bonuses,Name,ID")] Player player)
        {
            if (id != player.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdatePlayer(player, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterID"] = new SelectList(DAL.GetCharacters(), "ID", "Name", player.CharacterID);
            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player player = DAL.GetPlayer((int)id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeletePlayer(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
