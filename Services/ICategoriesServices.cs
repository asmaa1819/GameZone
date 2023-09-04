namespace GameZone.Services
{
    public interface ICategoriesServices
    {
        IEnumerable<SelectListItem> GetSelectLists();
    }
}
