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
    public class ConditionController : Controller
    {
        // GET: Condition
        public ActionResult Index()
        {
            return View(DAL.GetConditions());
        }

        // GET: Condition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Condition condition = DAL.GetCondition((int)id);
            if (condition == null)
            {
                return NotFound();
            }

            return View(condition);
        }

        // GET: Condition/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Condition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Effect,Name,ID")] Condition condition)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateCondition(condition) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(condition);
        }

        // GET: Condition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Condition condition = DAL.GetCondition((int)id);
            if (condition == null)
            {
                return NotFound();
            }
            return View(condition);
        }

        // POST: Condition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Effect,Name,ID")] Condition condition)
        {
            if (id != condition.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateCondition(condition, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(condition);
        }

        // GET: Condition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Condition condition = DAL.GetCondition((int)id);
            if (condition == null)
            {
                return NotFound();
            }

            return View(condition);
        }

        // POST: Condition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteCondition(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// searches for a list of Conditions containing the search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IActionResult Search(string searchText) {
            List<Condition> conditions = new List<Condition>();
            List<Condition> allConditions = DAL.GetConditions();
            if(searchText == null || searchText == "") {
                return View("Index", allConditions);
            }
            foreach(Condition condition in allConditions) {
                if(condition.Name.ToLower().Contains(searchText.ToLower())) {
                    conditions.Add(condition);
                }
            }
            return View("Index", conditions);
        }
    }
}
