namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;

        }
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile; //CAST value from IFormFile to maintain the cover type 
            if (file != null)
            {
               
                if (file.Length>_maxFileSize)
                {
                    return new ValidationResult($"Maximun Allowed size is {_maxFileSize} bytes");

                }
            }
            return ValidationResult.Success;


        }
    }
}
