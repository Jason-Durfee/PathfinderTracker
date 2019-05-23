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
    public class FeatController : Controller
    {
        // GET: Feat
        public ActionResult Index()
        {
            return View(DAL.GetFeats());
        }

        // GET: Feat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Feat feat = DAL.GetFeat((int)id);
            if (feat == null)
            {
                return NotFound();
            }

            return View(feat);
        }

        // GET: Feat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description,Name,ID")] Feat feat)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateFeat(feat) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(feat);
        }

        // GET: Feat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Feat feat = DAL.GetFeat((int)id);
            if(feat == null)
            {
                return NotFound();
            }
            return View(feat);
        }

        // POST: Feat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Description,Name,ID")] Feat feat)
        {
            if (id != feat.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateFeat(feat, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(feat);
        }

        // GET: Feat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Feat feat = DAL.GetFeat((int)id);
            if(feat == null)
            {
                return NotFound();
            }

            return View(feat);
        }

        // POST: Feat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteFeat(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// searches for a list of feats containing the search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IActionResult Search(string searchText) {
            List<Feat> feats = new List<Feat>();
            List<Feat> allFeats = DAL.GetFeats();
            if(searchText == null || searchText == "") {
                return View("Index", allFeats);
            }
            foreach(Feat feat in allFeats) {
                if(feat.Name.ToLower().Contains(searchText.ToLower())) {
                    feats.Add(feat);
                }
            }
            return View("Index", feats);
        }
    }
}
