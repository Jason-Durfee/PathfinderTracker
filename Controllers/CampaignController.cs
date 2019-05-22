﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PathfinderTracker.Models;

namespace PathfinderTracker
{
    public class CampaignController : Controller
    {
        // GET: Campaign
        public ActionResult Index()
        {
            return View(DAL.GetCampaigns());
        }

        // GET: Campaign/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campaign campaign = DAL.GetCampaign((int)id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaign/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campaign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateCampaign(campaign) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(campaign);
        }

        // GET: Campaign/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campaign campaign = DAL.GetCampaign((int)id);
            if (campaign == null)
            {
                return NotFound();
            }
            return View(campaign);
        }

        // POST: Campaign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID")] Campaign campaign)
        {
            if (id != campaign.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateCampaign(campaign, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(campaign);
        }

        // GET: Campaign/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campaign campaign = DAL.GetCampaign((int)id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteCampaign(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}