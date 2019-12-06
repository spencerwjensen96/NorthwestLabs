using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Work_Order")]
    public class WorkOrder
    {
        [Key]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Order Date")]
        [Required(ErrorMessage = "Order Date is required")]
        public DateTime Order_Date { get; set; }

        [Display(Name = "Quoted Price")]
        public decimal Quoted_Price { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("WorkOrderStatus")]
        public int StatusId { get; set; }
        public virtual WorkOrderStatus WorkOrderStatus { get; set; }

        [Display(Name = "Client")]
        [Required(ErrorMessage = "Client is required")]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Display(Name = "Data Report")]
        public string Data_Report_URL { get; set; }

        [Display(Name = "Summary Report")]
        public string Summary_Report_URL { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount_Percent { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Confirmation Sent")]
        public DateTime? Confirmation_Sent { get; set; }

        public virtual ICollection<Compound> Compounds { get; set; }

    }
}