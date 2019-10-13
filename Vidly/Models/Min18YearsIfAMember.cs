using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    //Custom Validation
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var customer = (Customer)validationContext.ObjectInstance;

            //Pay as you go does not require minimum age
            if (customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.Birthdate == null)
                return new ValidationResult("Birthdate is required.");

            //Fix this to be more accurate
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            //TimeSpan diff = DateTime.Today.Subtract(customer.Birthdate.Value);
        
            return (age >= 18)
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 years or older");
        }

    }
}