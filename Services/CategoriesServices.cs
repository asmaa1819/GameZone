using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ApplicationDbContext _context;

        public CategoriesServices(ApplicationDbContext context)
        {
            _context = context;
        }
        IEnumerable<SelectListItem> ICategoriesServices.GetSelectLists()
        {
            return _context.Categories
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .OrderBy(c => c.Text)
            .AsNoTracking()
            .ToList();
        }
    }
}
