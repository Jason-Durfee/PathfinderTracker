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
        public ActionResult Create([Bind("Name,ID,CurrentTime,Notes")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                if(campaign.Notes == null) {
                    campaign.Notes = "";
                }
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

        public ActionResult AddHours(int hours) {
            Campaign campaign = CurrentVariables.CurrentCampaign;
            if(campaign == null) {
                return NotFound();
            }
            campaign.AddHours(hours);
            CurrentVariables.TimeSinceWeatherChange += hours;
            if(CurrentVariables.TimeSinceWeatherChange >= 3) {
                //change weather type
                RandomWeatherSelect();
            }
            return RedirectToAction(nameof(Index));
        }

        private static void RandomWeatherSelect() {
            List<WeatherType> weatherTypes = DAL.GetWeatherTypes();
            int currentSeed = DateTime.Now.Millisecond;
            Random rand = new Random(currentSeed);
            int randNumber = rand.Next(0, 101);
            foreach(WeatherType weatherType in weatherTypes) {
                if(randNumber >= weatherType.MinSelector && randNumber <= weatherType.MaxSelector) {
                    CurrentVariables.CurrentWeatherType = weatherType;
                }
            }
            CurrentVariables.TimeSinceWeatherChange = 0;
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
        public ActionResult Edit(int id, [Bind("Name,ID,CurrentTime,Notes")] Campaign campaign)
        {
            if (id != campaign.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(campaign.Notes == null) {
                    campaign.Notes = "";
                }
                Campaign tempCampaign = DAL.GetCampaign(id);
                campaign.OriginalStartDate = tempCampaign.OriginalStartDate;
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
                if(CurrentVariables.CurrentCampaignID == id) {
                    CurrentVariables.CurrentCampaignID = -1;
                }
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// sets a selected campaign as the current one
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Load(int id) {
            if(id > 0) {
                CurrentVariables.CurrentCampaign = null;
                CurrentVariables.CurrentCampaignID = id;
                RandomWeatherSelect();
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// searches for a list of campaigns containing the search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public IActionResult Search(string searchText) {
            List<Campaign> campaigns = new List<Campaign>();
            List<Campaign> allCampaigns = DAL.GetCampaigns();
            if(searchText == null || searchText == "") {
                return View("Index", allCampaigns);
            }
            foreach(Campaign campaign in allCampaigns) {
                if(campaign.Name.ToLower().Contains(searchText.ToLower())) {
                    campaigns.Add(campaign);
                }
            }
            return View("Index", campaigns);
        }
    }
}
