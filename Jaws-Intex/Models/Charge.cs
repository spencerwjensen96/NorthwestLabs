using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Charge")]
    public class Charge
    {
        [Key]
        public int ChargeId { get; set; }

        [Display(Name = "Work Order")]
        [Required]
        [ForeignKey("WorkOrder")]
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }


    }
}