using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        [Display(Name = "Client Id")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Street Address")]
        public string Address_1 { get; set; }

        [Display(Name = "Apt/Suite/Other")]
        public string Address_2 { get; set; }

        [Required(ErrorMessage = "Client Contact Name is required")]
        [Display(Name = "Client Contact Name")]
        public string Primary_Contact { get; set; }

        [Display(Name = "Primary Phone")]
        [Required(ErrorMessage = "Primary Phone is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "Phone number format 555-555-5555")]
        public string Phone_1 { get; set; }

        [Display(Name = "Secondary Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "Phone number format 555-555-5555")]
        public string Phone_2 { get; set; }

        [Display(Name = "Primary Email")]
        [Required(ErrorMessage = "Primary Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email_1 { get; set; }

        [Display(Name = "Secondary Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email_2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required")]
        [RegularExpression(@"^(?:(A[KLRZ]|C[AOT]|D[CE]|FL|GA|HI|I[ADLN]|K[SY]|LA|M[ADEINOST]|N[CDEHJMVY]|O[HKR]|P[AR]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY]))$", ErrorMessage = "Please US state abbr.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code is required")]
        [RegularExpression(@"^\d{5}([\-]\d{4})?$", ErrorMessage = "Please enter valid Zip Code")]
        public string Zip { get; set; }

        [Display(Name = "Payment Info")]
        public string Payment_Info { get; set; }

        public ICollection<WorkOrder> WorkOrders { get; set; }

    }
}