using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Addres 1 is required, Address 2 is optional")]
        [Display(Name = "Address 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "City name is required")]
        public string City { get; set; }
        [Display(Name = "State code")]
        [Required(ErrorMessage = "State code (2 char) is required")]
        [MinLength(2)]
        public string StateCode { get; set; }
        [MinLength(4, ErrorMessage = "Zip Code required, min. 5 chars.")]
        public string Zip { get; set; }
        [Required(ErrorMessage="Address type is required")]
        public string AddressType { get; set; }

        public virtual IList<Contact> ContactList { get; set; }

        public override string ToString()
        {
            return $"Type: {AddressType} {AddressLine1}, {AddressLine2}, {City}, {StateCode}, {Zip}";
        }
    }
}