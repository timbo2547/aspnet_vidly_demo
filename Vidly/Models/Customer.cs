using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        //Data Annotations
        //[Required]
        //[StringLength(255)]
        //[Range(1, 10)]
        //[Compare("OtherProperty")]
        //[Phone]
        //[EmailAddress]
        //[Url]
        //[RegularExpression("...")]


        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
        //[Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}