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
    public class WeatherTypeController : Controller
    {
        // GET: WeatherType
        public ActionResult Index()
        {
            return View(DAL.GetWeatherTypes());
        }

        // GET: WeatherType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeatherType WeatherType = DAL.GetWeatherType((int)id);
            if (WeatherType == null)
            {
                return NotFound();
            }

            return View(WeatherType);
        }

        // GET: WeatherType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeatherType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,ID,MinSelector,MaxSelector")] WeatherType WeatherType)
        {
            if (ModelState.IsValid)
            {
                if(DAL.CreateWeatherType(WeatherType) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(WeatherType);
        }

        // GET: WeatherType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeatherType WeatherType = DAL.GetWeatherType((int)id);
            if(WeatherType == null)
            {
                return NotFound();
            }
            return View(WeatherType);
        }

        // POST: WeatherType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,ID,MinSelector,MaxSelector")] WeatherType WeatherType)
        {
            if (id != WeatherType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(DAL.UpdateWeatherType(WeatherType, id) > 0) {
                    //success
                }
                else {
                    //error
                }
                return RedirectToAction(nameof(Index));
            }
            return View(WeatherType);
        }

        // GET: WeatherType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeatherType WeatherType = DAL.GetWeatherType((int)id);
            if(WeatherType == null)
            {
                return NotFound();
            }

            return View(WeatherType);
        }

        // POST: WeatherType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(DAL.DeleteWeatherType(id) > 0) {
                //success
            }
            else {
                //error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
