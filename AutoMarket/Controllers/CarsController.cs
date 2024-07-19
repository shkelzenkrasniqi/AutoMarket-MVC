using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMarket.Data;
using AutoMarket.Models;
using X.PagedList;
using Microsoft.AspNetCore.Identity;
using AutoMarket.ViewModel;
using Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AutoMarket.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CarsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Cars
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 4; 

            var cars = await _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarModel)
                .Include(c => c.Photos)
                .Include(c => c.User)
                .ToPagedListAsync(pageNumber, pageSize);

            return View(cars);

        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }
            var car = await _context.Cars
                .Include(c => c.Photos)
                .Include(c => c.CarBrand)
                .Include(c => c.CarModel)
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
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CarBrandId"] = new SelectList(_context.Brands, "Id", "BrandName");
            ViewData["CarModelId"] = new SelectList(_context.Models, "Id", "ModelName");
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(Color)));
            ViewBag.ConditionOptions = new SelectList(Enum.GetValues(typeof(Condition)));
            ViewBag.FuelTypeOptions = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.MileageOptions = new SelectList(Enum.GetValues(typeof(Mileage)));
            ViewBag.SeatsOptions = new SelectList(Enum.GetValues(typeof(CarSeats)));
            ViewBag.TransmissionTypeOptions = new SelectList(Enum.GetValues(typeof(TransmissionType)));
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstRegistration,EnginePower,Price,Features,Description,Location,CarBrandId,CarModelId,CarFuelType,CarColor,CarCondition,CarMileage,CarSeats,CarTransmissionType")] Car car, List<IFormFile> photos)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            car.User = currentUser;

            if (ModelState.IsValid)
            {
                if (photos != null && photos.Count > 0)
                {
                    car.Photos = new List<CarPhoto>();

                    foreach (var photo in photos)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await photo.CopyToAsync(memoryStream);
                            car.Photos.Add(new CarPhoto
                            {
                                PhotoData = memoryStream.ToArray(),
                                ContentType = photo.ContentType
                            });
                        }
                    }
                }
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarBrandId"] = new SelectList(_context.Brands, "Id", "BrandName");
            ViewData["CarModelId"] = new SelectList(_context.Models, "Id", "ModelName");
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(Color)));
            ViewBag.ConditionOptions = new SelectList(Enum.GetValues(typeof(Condition)));
            ViewBag.FuelTypeOptions = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.MileageOptions = new SelectList(Enum.GetValues(typeof(Mileage)));
            ViewBag.SeatsOptions = new SelectList(Enum.GetValues(typeof(CarSeats)));
            ViewBag.TransmissionTypeOptions = new SelectList(Enum.GetValues(typeof(TransmissionType)));
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarBrandId"] = new SelectList(_context.Brands, "Id", "BrandName", car.CarBrandId);
            ViewData["CarModelId"] = new SelectList(_context.Models, "Id", "ModelName", car.CarModelId);
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(Color)));
            ViewBag.ConditionOptions = new SelectList(Enum.GetValues(typeof(Condition)));
            ViewBag.FuelTypeOptions = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.MileageOptions = new SelectList(Enum.GetValues(typeof(Mileage)));
            ViewBag.SeatsOptions = new SelectList(Enum.GetValues(typeof(CarSeats)));
            ViewBag.TransmissionTypeOptions = new SelectList(Enum.GetValues(typeof(TransmissionType)));
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstRegistration,EnginePower,Price,Features,Description,Location,CarBrandId,CarModelId,CarFuelType,CarColor,CarCondition,CarMileage,CarSeats,CarTransmissionType")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["CarBrandId"] = new SelectList(_context.Brands, "Id", "BrandName", car.CarBrandId);
            ViewData["CarModelId"] = new SelectList(_context.Models, "Id", "ModelName", car.CarModelId);
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(Color)));
            ViewBag.ConditionOptions = new SelectList(Enum.GetValues(typeof(Condition)));
            ViewBag.FuelTypeOptions = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.MileageOptions = new SelectList(Enum.GetValues(typeof(Mileage)));
            ViewBag.SeatsOptions = new SelectList(Enum.GetValues(typeof(CarSeats)));
            ViewBag.TransmissionTypeOptions = new SelectList(Enum.GetValues(typeof(TransmissionType)));
            return View(car);
        }
        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarModel)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }


        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CarExists(int id)
        {
          return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> FilterCars(
    [FromQuery] int? brandId,
    [FromQuery] int? modelId,
    [FromQuery] FuelType? fuelType,
    [FromQuery] Color? color,
    [FromQuery] Condition? condition,
    [FromQuery] Mileage? mileage,
    [FromQuery] CarSeats? seats,
    [FromQuery] TransmissionType? transmissionType,
    [FromQuery] DateTime? StartDate,
    [FromQuery] DateTime? EndDate,
    [FromQuery] string sortByPrice,
    [FromQuery] float? minPrice,
    [FromQuery] float? maxPrice)
        {
            var viewModel = new FilterCarsViewModel
            {
                Brands = await _context.Brands.ToListAsync(),
                Models = await _context.Models.ToListAsync(),
                FilteredCars = new List<Car>()
            };

            // Check if any filter parameters are provided
            if (!brandId.HasValue &&
                !modelId.HasValue &&
                !fuelType.HasValue &&
                !color.HasValue &&
                !condition.HasValue &&
                !mileage.HasValue &&
                !seats.HasValue &&
                !transmissionType.HasValue &&
                !StartDate.HasValue &&
                !EndDate.HasValue &&
                string.IsNullOrEmpty(sortByPrice) &&
                !minPrice.HasValue &&
                !maxPrice.HasValue)
            {
                // No filter parameters provided, return the view model without any filtered cars
                return View(viewModel);
            }

            IQueryable<Car> query = _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarModel)
                .Include(c => c.Photos)
                .Include(c => c.User);

            if (brandId.HasValue)
            {
                query = query.Where(c => c.CarBrandId == brandId.Value);
            }
            if (modelId.HasValue)
            {
                query = query.Where(c => c.CarModelId == modelId.Value);
            }
            if (fuelType.HasValue)
            {
                query = query.Where(c => c.CarFuelType == fuelType.Value);
            }
            if (color.HasValue)
            {
                query = query.Where(c => c.CarColor == color.Value);
            }
            if (mileage.HasValue)
            {
                query = query.Where(c => c.CarMileage == mileage.Value);
            }
            if (seats.HasValue)
            {
                query = query.Where(c => c.CarSeats == seats.Value);
            }
            if (condition.HasValue)
            {
                query = query.Where(c => c.CarCondition == condition.Value);
            }
            if (transmissionType.HasValue)
            {
                query = query.Where(c => c.CarTransmissionType == transmissionType.Value);
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

            viewModel.FilteredCars = await query.ToListAsync();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetModelsByBrand(int brandId)
        {
            var models = _context.Models.Where(m => m.CarBrandId == brandId)
                                         .Select(m => new { value = m.Id, text = m.ModelName })
                                         .ToList();
            return Json(models);
        }
    }
}
