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
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            return View(DAL.GetItems());
        }

        // GET: Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = DAL.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            ViewData["SlotID"] = new SelectList(DAL.GetSlots(), "ID", "Name");
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description,ConstructionRequirements,GPValue,SlotID,Name,ID")] Item item)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateItem(item) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SlotID"] = new SelectList(DAL.GetSlots(), "ID", "Name", item.SlotID);
            return View(item);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = DAL.GetItem((int)id);
            if(item == null)
            {
                return NotFound();
            }
            ViewData["SlotID"] = new SelectList(DAL.GetSlots(), "ID", "Name", item.SlotID);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Description,ConstructionRequirements,GPValue,SlotID,Name,ID")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateItem(item, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SlotID"] = new SelectList(DAL.GetSlots(), "ID", "Name", item.SlotID);
            return View(item);
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = DAL.GetItem((int)id);
            if(item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteItem(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// searches for a list of Items containing the search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IActionResult Search(string searchText) {
            List<Item> items = new List<Item>();
            List<Item> allItems = DAL.GetItems();
            if(searchText == null || searchText == "") {
                return View("Index", allItems);
            }
            foreach(Item item in allItems) {
                if(item.Name.ToLower().Contains(searchText.ToLower())) {
                    items.Add(item);
                }
            }
            return View("Index", items);
        }
    }
}
