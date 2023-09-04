namespace GameZone.Attributes
{
    public class AllowedExtentionsAttribute :ValidationAttribute
    {
        private readonly string _allowedExtentions;

        public AllowedExtentionsAttribute(string allowedExtentions)
        {
            _allowedExtentions = allowedExtentions;
  
        }
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile; //CAST value from IFormFile to maintain the cover type 
            if(file != null)
            {
               var extention = Path.GetExtension(file.FileName);

               var isAllowed = _allowedExtentions.Split(separator:',').Contains(extention,StringComparer.OrdinalIgnoreCase);
               
                if (!isAllowed)
                {
                    return new ValidationResult($"Only {_allowedExtentions} are allowed!");

                }
            }
           return ValidationResult.Success;


        }
    }
}
