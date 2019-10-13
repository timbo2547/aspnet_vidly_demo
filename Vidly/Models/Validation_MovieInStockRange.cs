using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Validation_MovieInStockRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Retrive object to validate
            var movie = (Movie)validationContext.ObjectInstance;

            return (movie.NumberInStock >= 1 && movie.NumberInStock <= 20)
                ? ValidationResult.Success
                : new ValidationResult("Number In Stock should be between 1 and 20.");
        }
    }
}