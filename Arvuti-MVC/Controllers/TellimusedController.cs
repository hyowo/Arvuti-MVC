using Arvuti_MVC.Data;
using Arvuti_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arvuti_MVC.Controllers
{
    public class TellimusedController : Controller
    {
        ApplicationDbContext _context;

        public TellimusedController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Tellimused.ToList();
            return View(model);
        }

        public IActionResult Telli()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Telli([Bind("ID,Nimi,Arvuti,Monitor,Klaviatuur,Hiir,Lisainfo")] TellimusModel tellimusModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tellimusModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tellimusModel);
        }

        public IActionResult Detailid(int id)
        {
            var model =
                _context.Tellimused.Where(t => t.ID == id).FirstOrDefault();
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Muuda(int? id)
        {
            if (id == null || _context.Tellimused == null)
            {
                return NotFound();
            }

            var tellimusModel = await _context.Tellimused.FindAsync(id);
            if (tellimusModel == null)
            {
                return NotFound();
            }
            return View(tellimusModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Muuda(int id, [Bind("ID,Nimi,Arvuti,Monitor,Klaviatuur,Hiir,Lisainfo,AruvtiOlemas,MonitorOlemas,KlaviatuurOlemas,HiirOlemas,Pakitud,ValjaSaadetud")] TellimusModel tellimusModel)
        {
            if (id != tellimusModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tellimusModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TellimusExists(tellimusModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tellimusModel);
        }

        [Authorize]
        public async Task<IActionResult> Pooleli()
        {
            var model = await _context.Tellimused
                .Where(x => !x.AruvtiOlemas || !x.MonitorOlemas || !x.KlaviatuurOlemas || !x.HiirOlemas)
                .ToListAsync();
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> MargiOlemas(int id, string komponent)
        {
            if (!TellimusExists(id))
                return NotFound();
            var model = _context.Tellimused.Where(t => t.ID == id).FirstOrDefault();
            if (model == null)
                return NotFound();
            if (komponent.ToLower() == "arvuti")
            {
                model.AruvtiOlemas = true;
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            else if (komponent.ToLower() == "monitor")
            {
                model.MonitorOlemas = true;
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            else if (komponent.ToLower() == "klaviatuur")
            {
                model.KlaviatuurOlemas = true;
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            else if (komponent.ToLower() == "hiir")
            {
                model.HiirOlemas = true;
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Pooleli));
        }

        [Authorize]
        public async Task<IActionResult> Pakkimata()
        {
            var model = await _context.Tellimused
                .Where(x => x.AruvtiOlemas && x.MonitorOlemas && x.KlaviatuurOlemas && x.HiirOlemas && !x.Pakitud)
                .ToListAsync();
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Paki(int id)
        {
            if (!TellimusExists(id))
                return NotFound();
            var model = _context.Tellimused.Where(x => x.ID == id)
                .FirstOrDefault();
            if (model == null)
                return NotFound();
            model.Pakitud = true;
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Pakkimata));
        }

        [Authorize]
        public async Task<IActionResult> Valjastamata()
        {
            var model = await _context.Tellimused
                .Where(x => x.Pakitud && !x.ValjaSaadetud)
                .ToListAsync();
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Valjasta(int id)
        {
            if (!TellimusExists(id))
                return NotFound();
            var model = _context.Tellimused.Where(x => x.ID == id)
                .FirstOrDefault();
            if (model == null)
                return NotFound();
            model.ValjaSaadetud = true;
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Valjastamata));
        }

        public IActionResult Statistika()
        {
            var model = new StatistikaViewModel()
            {
                TellimusedKokku = _context.Tellimused.Count(),
                ValmisTellimused = _context.Tellimused.Where(x => x.ValjaSaadetud == true).Count()
            };
            return View(model);
        }
        
        private bool TellimusExists(int id)
        {
            return (_context.Tellimused?.Any(e => e.ID == id)).GetValueOrDefault();
        }        
    }
}
