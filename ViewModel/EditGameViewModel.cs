using GameZone.Attributes;

namespace GameZone.ViewModel
{
    public class EditGameViewModel : GameFormViewModel
    {
        public int Id { get; set; }

        public string ?CurrentCover{ get; set; }

        [AllowedExtentions(FileSettings.AllowedExtentions), MaxFileSize((FileSettings.MaxFileSizeInBytes))]
        public IFormFile ?Cover { get; set; } = default!;
    }
}

