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
        public int CompoundId { get; set; }

        [Display(Name = "LT")]
        public int LT { get; set; }

        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Please enter the Compound Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the Compound Quantity")]
        [Display(Name = "Quantity (mg)")]
        public decimal Quantity { get; set; }

        [Display(Name = "Date Arrived")]
        public DateTime Date_Arrived { get; set; }

        [Display(Name = "Received By")]
        public string Received_By { get; set; }

        [Display(Name = "Appearance")]
        public string Appearance { get; set; }

        [Display(Name = "Reported Weight (mg)")]
        public decimal Reported_Weight { get; set; }

        [Display(Name = "Molecular Mass")]
        public decimal Molecular_Mass { get; set; }

        [Display(Name = "Max Tolerated Dose (mg)")]
        public decimal Max_Tolerated_Dose { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? Due_Date { get; set; }
        
        public ICollection<Sample> Samples { get; set; }
        public ICollection<CompoundStatus> CompoundStatuses { get; set; }

    }
}