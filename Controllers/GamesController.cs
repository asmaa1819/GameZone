
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
    [HttpGet]
    public IActionResult Update(int id)
    {
        var game = _gameService.GetById(id);

        if (game is null)
            return NotFound();

        EditGameViewModel viewModel = new()
        {
            Id = id,
            Name = game.Name,
            Descreption = game.Descreption,
            CategoryId = game.CategoryId,
            SelectedDevices = game.Device.Select(d => d.DeviceId).ToList(),
            Categories = _categoriesServices.GetSelectLists(),
            Devices = _devicesServices.GetSelectLists(),
            CurrentCover = game.Cover
        };

        return View(viewModel);


    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(EditGameViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = _categoriesServices.GetSelectLists();
            model.Devices = _devicesServices.GetSelectLists();
            return View(model);
        }

        var game = await _gameService.Update(model);

        if (game is null)
            return BadRequest();

        return RedirectToAction(nameof(Index));

    }

    [HttpDelete]
    public IActionResult Delete (int id)
    {
        var isDeleted = _gameService.Delete(id);

        return isDeleted ? Ok() : BadRequest();

    }

 }

