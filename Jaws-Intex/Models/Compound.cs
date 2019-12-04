using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Compound")]
    public class Compound
    {
        [Key]
        [Display(Name = "Compound ID")]
        public int CompoundID { get; set; }

        [Display(Name = "LT")]
        public int LT { get; set; }

        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Please enter the Compound Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the Compound Quantity")]
        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Display(Name = "Date Arrived")]
        public DateTime Date_Arrived { get; set; }

        [Display(Name = "Received By")]
        public string Received_By { get; set; }

        [Display(Name = "Appearance")]
        public string Appearance { get; set; }

        [Display(Name = "Reported Weight")]
        public decimal Reported_Weight { get; set; }

        [Display(Name = "Molecular Mass")]
        public decimal Molecular_Mass { get; set; }

        [Display(Name = "Max Tolerated Dose")]
        public decimal Max_Tolerated_Dose { get; set; }
        

    }
}