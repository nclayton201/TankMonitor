using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TankMonitor.SiteDb;

namespace TankMonitor.Views
{
    [Authorize(Roles = "Administrator")]
    public class TanksController : Controller
    {
        private readonly SiteContext _context;

        public TanksController(SiteContext context)
        {
            _context = context;
        }

        // GET: Tanks
        public async Task<IActionResult> Index(int? id, string siteNumber)
        {
            var siteContext = _context.Tanks.Include(t => t.Site);
            ViewData["SiteId"] = id;
            ViewData["SiteNumber"] = siteNumber;
            return View(await siteContext.ToListAsync());


        }

        // GET: Tanks/Details/5
        public async Task<IActionResult> Details(int? id, int siteId, string siteNumber)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tank = await _context.Tanks
                .Include(t => t.Site)
                .FirstOrDefaultAsync(m => m.TankId == id);
            if (tank == null)
            {
                return NotFound();
            }
            ViewData["SiteId"] = siteId;
            ViewData["SiteNumber"] = siteNumber;
            return View(tank);
        }

        // GET: Tanks/Create
        public IActionResult Create(int? siteId, string siteNumber)
        {
            ViewData["SiteId"] = siteId;
            ViewData["SiteNumber"] = siteNumber;
            return View();
        }

        // POST: Tanks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TankId,TankNumber,TankSize,TankProduct,SiteId")] Tank tank, int siteId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = siteId, siteNumber = _context.Sites.Find(siteId).SiteNumber });
            }
            return View(tank);
        }

        // GET: Tanks/Edit/5
        public async Task<IActionResult> Edit(int? id, int siteId, string siteNumber)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tank = await _context.Tanks.FindAsync(id);
            if (tank == null)
            {
                return NotFound();
            }
            ViewData["SiteId"] = siteId;
            ViewData["SiteNumber"] = siteNumber;
            return View(tank);
        }

        // POST: Tanks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TankId,TankNumber,TankSize,TankProduct,SiteId")] Tank tank, int siteId)
        {
            if (id != tank.TankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TankExists(tank.TankId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = siteId, siteNumber = _context.Sites.Find(siteId).SiteNumber });
            }
            return View(tank);
        }

        // GET: Tanks/Delete/5
        public async Task<IActionResult> Delete(int? id, int siteId, string siteNumber)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tank = await _context.Tanks
                .Include(t => t.Site)
                .FirstOrDefaultAsync(m => m.TankId == id);
            if (tank == null)
            {
                return NotFound();
            }
            ViewData["SiteId"] = siteId;
            ViewData["SiteNumber"] = siteNumber;
            return View(tank);
        }

        // POST: Tanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var tank = await _context.Tanks.FindAsync(id);
            int siteId = tank.SiteId;
            _context.Tanks.Remove(tank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = siteId, siteNumber = _context.Sites.Find(siteId).SiteNumber });
        }

        private bool TankExists(int id)
        {
            return _context.Tanks.Any(e => e.TankId == id);
        }
    }
}
