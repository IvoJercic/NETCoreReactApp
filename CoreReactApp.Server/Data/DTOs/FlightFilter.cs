using System;
using System.ComponentModel.DataAnnotations;

namespace CoreReactApp.Server.Data.DTOs
{
    public class FlightFilter
    {
        [Required(ErrorMessage = "SourceIATA is required.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "SourceIATA must be exactly 3 characters.")]
        public string? SourceIATA { get; set; }

        [Required(ErrorMessage = "DestinationIATA is required.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "DestinationIATA must be exactly 3 characters.")]
        public string? DestinationIATA { get; set; }

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(FlightFilter), nameof(ValidateEndDate))]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Passengers are required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Passengers must be greater than 0.")]
        public int? Passengers { get; set; }

        [CustomValidation(typeof(FlightFilter), nameof(ValidateCurrency))]
        public string? Currency { get; set; }

        public static ValidationResult? ValidateEndDate(DateTime? endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as FlightFilter;
            if (endDate.HasValue && instance?.StartDate.HasValue == true && endDate < instance.StartDate)
            {
                return new ValidationResult("EndDate must be greater than or equal to StartDate.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateCurrency(string? currency, ValidationContext context)
        {
            if (currency == null)
            {
                return ValidationResult.Success;
            }

            if (currency.Length == 3)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Currency must contain exactly 3 characters.");
        }
    }
}
