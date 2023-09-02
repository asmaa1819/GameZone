
namespace GameZone.Controllers;

 public class GamesController : Controller
 {
    private readonly ApplicationDbContext _context;

    public GamesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {


        CreateGameFormViewModel viewModel = new()
        {
            Categories = _context.Categories
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .OrderBy(c=>c.Text)
            .ToList(),

            Devices = _context.Devices
            .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
            .OrderBy(d => d.Text)
            .ToList()
        };

        

            return View(viewModel);
        }
 }

