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
        [RegularExpression(@"(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", 
            ErrorMessage ="The date must be entered as MM/DD/YYYY")]
        public DateTime Order_Date { get; set; }

        [Display(Name = "Quoted Price")]
        public decimal Quoted_Price { get; set; }

        [Display(Name = "Amount Due")]
        public decimal Amount_Due { get; set; }

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

    }
}