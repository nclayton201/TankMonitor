using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TankMonitor.Models;
using TankMonitor.SiteDb;

namespace TankMonitor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ITankProcessor tankProcessor;
        private IConfiguration Configuration { get; set; }
        public HomeController(IConfiguration configuration, ITankProcessor tankProcessor)
        {
            this.tankProcessor = tankProcessor;
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            List<Site> sites = tankProcessor.FetchSites();
            List<Inventory> latestInventories = new List<Inventory>();

            foreach (var site in sites) {
                List<Inventory> fullInventories = tankProcessor.FetchInventories(site.SiteId);
                latestInventories.AddRange(tankProcessor.LatestInventories(fullInventories));
            }

            // Convert data to Json, preserving references so it can be serialized
            ViewBag.SiteDataSource = JsonConvert.SerializeObject(sites, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            }); ;
            ViewBag.LatestInventories = JsonConvert.SerializeObject(latestInventories, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            }); ; ;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Sites()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View("View/Sites/Create.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
