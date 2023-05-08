using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Validations
{
    public class FiveWords : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldvalue = (string)value;
            var arr = fieldvalue.Split(" ");
            if(fieldvalue == null || arr.Length < 5)
            {
                return new ValidationResult("Description need to have at least 5 words");
            }

            return ValidationResult.Success;
        }
    }
}
