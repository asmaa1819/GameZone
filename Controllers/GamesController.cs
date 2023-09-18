
namespace GameZone.Controllers;

 public class GamesController : Controller
 {
    
    private readonly ICategoriesServices _categoriesServices;
    private readonly IDevicesService _devicesServices;
    private readonly IGamesService _gameService;



    public GamesController(ICategoriesServices categoriesServices, IDevicesService devicesServices, IGamesService gameService)
    {

        _categoriesServices = categoriesServices;
        _devicesServices = devicesServices;
        _gameService = gameService;
    }

    public IActionResult Index()
    {
        var games = _gameService.GetAll();
        return View(games);
    }
    public IActionResult Details(int id)
    {
        var game = _gameService.GetById(id);
        if (game is null)
            return NotFound();
        return View(game);
    }

    public IActionResult Create()
    {


        CreateGameFormViewModel viewModel = new()
        {
            Categories = _categoriesServices.GetSelectLists(),
            Devices = _devicesServices.GetSelectLists()
        };

        

            return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateGameFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = _categoriesServices.GetSelectLists();
            model.Devices = _devicesServices.GetSelectLists();
            return View(model);
        }
        await _gameService.Create(model);

        return RedirectToAction(nameof(Index)) ;

    }

 }

