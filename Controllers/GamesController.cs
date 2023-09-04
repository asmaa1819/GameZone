
namespace GameZone.Controllers;

 public class GamesController : Controller
 {
    
    private readonly ICategoriesServices _categoriesServices;
    private readonly IDevicesService _devicesServices;
    private readonly IGamesService _gameServices;



    public GamesController(ICategoriesServices categoriesServices, IDevicesService devicesServices, IGamesService gameServices)
    {

        _categoriesServices = categoriesServices;
        _devicesServices = devicesServices;
        _gameServices = gameServices;
    }

    public IActionResult Index()
        {
            return View();
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
        await _gameServices.Create(model);

        return RedirectToAction(nameof(Index)) ;

    }

 }

