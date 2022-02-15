using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            if (HttpContext.Session.GetInt32("Fullness") == null)
            {
                HttpContext.Session.SetInt32("Fullness", 20);
            }
            if (HttpContext.Session.GetInt32("Happiness") == null)
            {
                HttpContext.Session.SetInt32("Happiness", 20);
            }
            if (HttpContext.Session.GetInt32("Meals") == null)
            {
                HttpContext.Session.SetInt32("Meals", 3);
            }
            if (HttpContext.Session.GetInt32("Energy") == null)
            {
                HttpContext.Session.SetInt32("Energy", 50);
            }
                ViewBag.Full = HttpContext.Session.GetInt32("Fullness");
                ViewBag.Happy = HttpContext.Session.GetInt32("Happiness");
                ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
                ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
                ViewBag.Message = TempData["Message"];
            return View("Index");
        }
        [HttpPost("/feed")]
        public IActionResult Feed()
        {

            if (HttpContext.Session.GetInt32("Meals") > 0)
            {
                Random rand = new Random();
                int randNum = rand.Next(5,10);
                if (randNum >= 6)
                {
                    int? Counter = HttpContext.Session.GetInt32("Fullness");
                    Counter += randNum;
                    HttpContext.Session.SetInt32("Fullness", (int)Counter);
                    int? Decrement = HttpContext.Session.GetInt32("Meals");
                    Decrement -= 1;
                    HttpContext.Session.SetInt32("Meals", (int)Decrement);
                    TempData["Message"] = $"You fed your pet -- You're not a monster, after all! Fullness +{randNum}, Meals -1.";
                    return RedirectToAction ("Index");
                }
                else
                {
                    int? Decrement = HttpContext.Session.GetInt32("Meals");
                    Decrement -= 1;
                    HttpContext.Session.SetInt32("Meals", (int)Decrement);
                    TempData["Message"] = "Your pet got sick! Meals -1";
                    return RedirectToAction ("Index");

                }
            }
            TempData["Message"] = "You can't do that -- gotta work for them meals, first!";
            return RedirectToAction("Index");
        }

        [HttpPost("/play")]
        public IActionResult Play()
        {
            if (HttpContext.Session.GetInt32("Energy") >= 5)
            {
                Random rand = new Random();
                int randNum = rand.Next(5,10);
                if (randNum >=6)
                {
                    int? Counter = HttpContext.Session.GetInt32("Happiness");
                    Counter += randNum;
                    HttpContext.Session.SetInt32("Happiness", (int)Counter);
                    int? Decrement = HttpContext.Session.GetInt32("Energy");
                    Decrement -= 5;
                    HttpContext.Session.SetInt32("Energy", (int)Decrement);
                    TempData["Message"] = $"You played with your pet -- No need to call ASPCA, now! Happiness +{randNum}, Energy -5.";
                    return RedirectToAction ("Index");
                }
                else
                {
                    int? Decrement = HttpContext.Session.GetInt32("Energy");
                    Decrement -= 5;
                    HttpContext.Session.SetInt32("Energy", (int)Decrement);
                    TempData["Message"] = "Your doesn't feel like playing right now! Energy -5.";
                    return RedirectToAction ("Index");

                }
            }
            return View("Index");

        }

        [HttpPost("/work")]
        public IActionResult Work()
        {
            if (HttpContext.Session.GetInt32("Energy") >= 5)
            {
                int? Counter = HttpContext.Session.GetInt32("Meals");
                Counter += 3;
                HttpContext.Session.SetInt32("Meals", (int)Counter);
                int? Decrement = HttpContext.Session.GetInt32("Energy");
                Decrement -= 5;
                HttpContext.Session.SetInt32("Energy", (int)Decrement);
                TempData["Message"] = "You...put your pet to work? Went to work? Not sure if this is poblematic or not. Meals +3, Energy -5.";
                return RedirectToAction ("Index");
            }
            return View("Index");

        }
        
        [HttpPost("/sleep")]
        public IActionResult Sleep()
        {
            if (HttpContext.Session.GetInt32("Energy") > 0)
            {
                int? Counter = HttpContext.Session.GetInt32("Energy");
                Counter += 15;
                HttpContext.Session.SetInt32("Energy", (int)Counter);

                // Decrease both Fullness and Happiness
                int? Decrement = HttpContext.Session.GetInt32("Fullness");
                Decrement -= 5;
                HttpContext.Session.SetInt32("Fullness", (int)Decrement);

                Decrement = HttpContext.Session.GetInt32("Happiness");
                Decrement -= 5;
                HttpContext.Session.SetInt32("Happiness", (int)Decrement);
                TempData["Message"] = "You encourage your pet to sleep. Try NyQuil. Energy +15, Fullness and Happiness -5.";

                return RedirectToAction ("Index");
            }
            return View("Index");

        }

        [HttpPost("/reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
