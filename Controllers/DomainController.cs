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
    public class DomainController : Controller
    {
        // GET: Domain
        public ActionResult Index()
        {
            return View(DAL.GetDomains());
        }

        // GET: Domain/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Domain domain = DAL.GetDomain((int)id);
            if (domain == null)
            {
                return NotFound();
            }

            return View(domain);
        }

        // GET: Domain/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Domain/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description,Name,ID")] Domain domain)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateDomain(domain) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(domain);
        }

        // GET: Domain/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Domain domain = DAL.GetDomain((int)id);
            if(domain == null)
            {
                return NotFound();
            }
            return View(domain);
        }

        // POST: Domain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Description,Name,ID")] Domain domain)
        {
            if (id != domain.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateDomain(domain, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(domain);
        }

        // GET: Domain/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Domain domain = DAL.GetDomain((int)id);
            if(domain == null)
            {
                return NotFound();
            }

            return View(domain);
        }

        // POST: Domain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteDomain(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
