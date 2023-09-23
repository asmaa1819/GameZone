using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModel
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        
        [AllowedExtentions(FileSettings.AllowedExtentions),MaxFileSize((FileSettings.MaxFileSizeInBytes))]
        public IFormFile Cover { get; set; } = default!;
    }
}
