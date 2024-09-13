using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsRegistry.Attributes
{
    public class MinDateToday : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToDateTime(value) > DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Date must me greater than current date!");
        }
    }
}