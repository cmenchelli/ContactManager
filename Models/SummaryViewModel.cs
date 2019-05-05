using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class SummaryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Number of contacts")]
        public  int Contacts { get; set; }
        [Display(Name ="Total addresses")]
        public int Addresses { get; set; }
        [Display(Name ="Home addresses")]
        public int HomeAddresses { get; set; }
        [Display(Name ="Business addresses")]
        public int BusinessAddresses { get; set; }
        [Display(Name = "Other addresses")]
        public int OtherAddresses { get; set; }
        [Display(Name = "Number of computers")]
        public int Computers { get; set; }
    }
}