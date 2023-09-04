

namespace GameZone.Services
{
    public class GameService : IGamesService
    {
        private readonly ApplicationDbContext _context; 
        private readonly IWebHostEnvironment _webHostEnvironment; // to determin the place where the img be
        private readonly string _imgesPath;
        public GameService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imgesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var CoverName =$"{ Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_imgesPath, CoverName);
            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);
            

            Game game = new()
            {
                Name = model.Name,
                Descreption = model.Descreption,
                CategoryId = model.CategoryId,
                Cover = CoverName,
                Device=model.SelectedDevices.Select(d => new GameDevice { DeviceId =d} ).ToList()


            };

            _context.Add(game);
            _context.SaveChanges();
        }
    }
}
