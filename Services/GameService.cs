

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
        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                 .Include(g => g.Category)
                 .Include(g => g.Device)
                 .ThenInclude(d => d.Device)
                 .AsNoTracking()
                 .ToList();

        }
        public Game? GetById(int id)
        {
            return _context.Games
             .Include(g => g.Category)
             .Include(g => g.Device)
             .ThenInclude(d => d.Device)
             .AsNoTracking()
             .SingleOrDefault(g => g.Id == id);


        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var CoverName = await SaveCover(model.Cover);


            Game game = new()
            {
                Name = model.Name,
                Descreption = model.Descreption,
                CategoryId = model.CategoryId,
                Cover = CoverName,
                Device = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()


            };

            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(EditGameViewModel model)
        {
            
            var game = _context.Games  // to ensure that the id is exsist and true
                .Include(g =>g.Device)
                .SingleOrDefault(g =>g.Id == model.Id);

            if (game is null)
                return null;

            var HasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Descreption = model.Descreption;
            game.CategoryId = model.CategoryId;
            game.Device = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (HasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }
            var effectedRows=_context.SaveChanges();

            if (effectedRows > 0) // this mean there is update done
            {
                if (HasNewCover)
                {
                    var cover = Path.Combine(_imgesPath, oldCover);
                    File.Delete(cover);

                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imgesPath, game.Cover);
                File.Delete(cover);

                return null;
            }
        }
        public bool Delete(int id)
        {
            var IsDeleted = false;
            var game = _context.Games.Find(id);

            if(game is null) 
                return IsDeleted;

            _context.Remove(game);

            var EffectRows = _context.SaveChanges();
            if(EffectRows>0)
            {
                IsDeleted = true;
                var cover = Path.Combine(_imgesPath, game.Cover);
                File.Delete(cover);
            }
            return IsDeleted;
        }
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imgesPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }

    }


}
