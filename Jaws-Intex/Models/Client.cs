using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    public class Client
    {
        [Display(Name ="Client Id")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Primary Address is required")]
        [Display(Name = "Primary Address")]
        public string Address_1 { get; set; }

        [Display(Name = "Secondary Address")]
        public string Address_2 { get; set; }

        [Required(ErrorMessage = "Client Contact Name is required")]
        [Display(Name = "Client Contact Name")]
        public string Primary_Contact { get; set; }

        [Required(ErrorMessage = "Primary Phone is required")]
        [Display(Name = "Primary Phone")]
        public string Phone_1 { get; set; }

        [Display(Name = "Secondary Phone")]
        public string Phone_2 { get; set; }

        [Required(ErrorMessage = "Primary Email is required")]
        [Display(Name = "Primary Email")]
        public string Email_1 { get; set; }

        [Display(Name = "Secondary Email")]
        public string Email_2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [Display(Name = "Zip Code")]
        public string Zip { get; set; }






    }
}