using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Display(Name = "F.name")]
        public string FirstName { get; set; }
        [Display(Name = "L.name")]
        public string LastName { get; set; }
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime Birthdate { get; set; }
        [Display(Name = "# of computers")]
        public int NumberOfComputers { get; set; }
        //public int count { get; set; }

       // public int AddressID { get; set; }
        public virtual IList<Address> AddressList { get; set; }
    }
}