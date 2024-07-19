using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMarket.Data;
using AutoMarket.Models;
using X.PagedList;
using Microsoft.AspNetCore.Identity;
using AutoMarket.ViewModel;
using Models.Enums;

namespace AutoMarket.Controllers
{
    public class MotorcyclesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public MotorcyclesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Cars
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 4;

            var motorcycle = await _context.Motorcycles
                .Include(c => c.MotorcycleBrand)
                .Include(c => c.MotorcycleModel)
                .Include(c => c.Photos)
                .Include(c => c.User)
                .ToPagedListAsync(pageNumber, pageSize);

            return View(motorcycle);

        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Motorcycles == null)
            {
                return NotFound();
            }
            var car = await _context.Motorcycles
                .Include(c => c.Photos)
                .Include(c => c.MotorcycleBrand)
                .Include(c => c.MotorcycleModel)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car != null && car.User != null)
            {
                var userId = car.User.Id;
            }
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["MotorcycleBrandId"] = new SelectList(_context.MotorcycleBrands, "Id", "BrandName");
            ViewData["MotorcycleModelId"] = new SelectList(_context.MotorcycleModels, "Id", "ModelName");
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(Color)));
            ViewBag.ConditionOptions = new SelectList(Enum.GetValues(typeof(Condition)));
            ViewBag.FuelTypeOptions = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.MileageOptions = new SelectList(Enum.GetValues(typeof(Mileage)));
            ViewBag.TypeOptions = new SelectList(Enum.GetValues(typeof(MotorcycleType)));
            ViewBag.TransmissionTypeOptions = new SelectList(Enum.GetValues(typeof(TransmissionType)));
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstRegistration,EnginePower,Price,Features,Description,Location,MotorcycleBrandId,MotorcycleModelId,FuelType,Color,Condition,Mileage,MotorcycleType,TransmissionType")] Motorcycle motorcycle, List<IFormFile> photos)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            motorcycle.User = currentUser;

            if (ModelState.IsValid)
            {
                if (photos != null && photos.Count > 0)
                {
                    motorcycle.Photos = new List<MotorcyclePhoto>();

                    foreach (var photo in photos)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await photo.CopyToAsync(memoryStream);
                            motorcycle.Photos.Add(new MotorcyclePhoto
                            {
                                PhotoData = memoryStream.ToArray(),
                                ContentType = photo.ContentType
                            });
                        }
                    }
                }
                _context.Add(motorcycle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MotorcycleBrandId"] = new SelectList(_context.MotorcycleBrands, "Id", "BrandName");
            ViewData["MotorcycleModelId"] = new SelectList(_context.MotorcycleModels, "Id", "ModelName");
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(Color)));
            ViewBag.ConditionOptions = new SelectList(Enum.GetValues(typeof(Condition)));
            ViewBag.FuelTypeOptions = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.MileageOptions = new SelectList(Enum.GetValues(typeof(Mileage)));
            ViewBag.TypeOptions = new SelectList(Enum.GetValues(typeof(MotorcycleType)));
            ViewBag.TransmissionTypeOptions = new SelectList(Enum.GetValues(typeof(TransmissionType)));
            return View(motorcycle);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Motorcycles == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles.FindAsync(id);
            if (motorcycle == null)
            {
                return NotFound();
            }
            ViewData["MotorcycleBrandId"] = new SelectList(_context.MotorcycleBrands, "Id", "BrandName", motorcycle.MotorcycleBrandId);
            ViewData["MotorcycleModelId"] = new SelectList(_context.MotorcycleModels, "Id", "ModelName", motorcycle.MotorcycleModelId);
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(Color)));
            ViewBag.ConditionOptions = new SelectList(Enum.GetValues(typeof(Condition)));
            ViewBag.FuelTypeOptions = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.MileageOptions = new SelectList(Enum.GetValues(typeof(Mileage)));
            ViewBag.TypeOptions = new SelectList(Enum.GetValues(typeof(MotorcycleType)));
            ViewBag.TransmissionTypeOptions = new SelectList(Enum.GetValues(typeof(TransmissionType)));
            return View(motorcycle);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstRegistration,EnginePower,Price,Features,Description,Location,MotorcycleBrandId,MotorcycleModelId,FuelType,Color,Condition,Mileage,MotorcycleType,TransmissionType")] Motorcycle motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorcycle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorcycleExists(motorcycle.Id))
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
            ViewData["MotorcycleBrandId"] = new SelectList(_context.MotorcycleBrands, "Id", "BrandName", motorcycle.MotorcycleBrandId);
            ViewData["MotorcycleModelId"] = new SelectList(_context.MotorcycleModels, "Id", "ModelName", motorcycle.MotorcycleModelId);
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(Color)));
            ViewBag.ConditionOptions = new SelectList(Enum.GetValues(typeof(Condition)));
            ViewBag.FuelTypeOptions = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.MileageOptions = new SelectList(Enum.GetValues(typeof(Mileage)));
            ViewBag.TypeOptions = new SelectList(Enum.GetValues(typeof(MotorcycleType)));
            ViewBag.TransmissionTypeOptions = new SelectList(Enum.GetValues(typeof(TransmissionType)));
            return View(motorcycle);
        }
        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Motorcycles == null)
            {
                return NotFound();
            }

            var car = await _context.Motorcycles
                .Include(c => c.MotorcycleBrand)
                .Include(c => c.MotorcycleModel)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }


        // POST: Motorcycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Motorcycles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Motorcycles'  is null.");
            }
            var motorcycle = await _context.Motorcycles.FindAsync(id);
            if (motorcycle != null)
            {
                _context.Motorcycles.Remove(motorcycle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool MotorcycleExists(int id)
        {
            return (_context.Motorcycles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> FilterMotorcycles(
        [FromQuery] int? brandId,
        [FromQuery] int? modelId,
        [FromQuery] FuelType? fuelType,
        [FromQuery] Color? color,
        [FromQuery] Condition? condition,
        [FromQuery] Mileage? mileage,
        [FromQuery] MotorcycleType? type,
        [FromQuery] TransmissionType? transmissionType,
        [FromQuery] DateTime? StartDate,
        [FromQuery] DateTime? EndDate,
        [FromQuery] string sortByPrice,
        [FromQuery] float? minPrice,
        [FromQuery] float? maxPrice
        )
        {
            IQueryable<Motorcycle> query = _context.Motorcycles
                .Include(c => c.MotorcycleBrand)
                .Include(c => c.MotorcycleModel)
                .Include(c => c.Photos)
                .Include(c => c.User);

            if (brandId.HasValue)
            {
                query = query.Where(c => c.MotorcycleBrandId == brandId.Value);
            }
            if (modelId.HasValue)
            {
                query = query.Where(c => c.MotorcycleModelId == modelId.Value);
            }
            if (fuelType.HasValue)
            {
                query = query.Where(c => c.FuelType == fuelType.Value);
            }
            if (color.HasValue)
            {
                query = query.Where(c => c.Color == color.Value);
            }
            if (mileage.HasValue)
            {
                query = query.Where(c => c.Mileage == mileage.Value);
            }
            if (type.HasValue)
            {
                query = query.Where(c => c.MotorcycleType == type.Value);
            }
            if (condition.HasValue)
            {
                query = query.Where(c => c.Condition == condition.Value);
            }
            if (transmissionType.HasValue)
            {
                query = query.Where(c => c.TransmissionType == transmissionType.Value);
            }

            if (StartDate.HasValue && EndDate.HasValue)
            {
                query = query.Where(c => c.FirstRegistration >= StartDate.Value && c.FirstRegistration <= EndDate.Value);
            }
            if (minPrice.HasValue)
            {
                query = query.Where(c => c.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(c => c.Price <= maxPrice.Value);
            }

            // Sorting
            switch (sortByPrice)
            {
                case "lowToHigh":
                    query = query.OrderBy(c => c.Price);
                    break;
                case "highToLow":
                    query = query.OrderByDescending(c => c.Price);
                    break;

                default:
                    break;
            }

            var filteredMotorcycles = await query.ToListAsync();

            var viewModel = new FilterMotorcyclesViewModel
            {
                Brands = await _context.MotorcycleBrands.ToListAsync(),
                Models = await _context.MotorcycleModels.ToListAsync(),
                FilteredMotorcycles = filteredMotorcycles
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult GetModelsByBrand(int brandId)
        {
            var models = _context.MotorcycleModels.Where(m => m.MotorcycleBrandId == brandId)
                                         .Select(m => new { value = m.Id, text = m.ModelName })
                                         .ToList();
            return Json(models);
        }
    }
}
