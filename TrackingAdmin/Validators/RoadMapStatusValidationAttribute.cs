using BLL.TrackingAdmin.Extensions;
using System.ComponentModel.DataAnnotations;

namespace TrackingAdmin.Validators
{
    public class RoadMapStatusValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string status = value as string;
            string allowedStatus = "Created,InProgress,Finished,Canceled";

            if (status.IsNullOrEmpty())
            {
                return ValidationResult.Success;
            }

            if (!allowedStatus.Contains(status))
            {
                return new ValidationResult("El valor del parámetro status debe ser 'Created', 'InProgress', 'Finished' o 'Canceled'");
            }

            return ValidationResult.Success;
        }
    }
}
