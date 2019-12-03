using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("WorkOrder")]
    public class WorkOrder
    {
        [Display(Name = "Order ID")]
        [Required(ErrorMessage = "Order ID is required")]
        public int OrderId { get; set; }

        [Display(Name = "Order Date")]
        [Required(ErrorMessage = "Order Date is required")]
        [RegularExpression(@"(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", 
            ErrorMessage ="The date must be entered as MM/DD/YYYY")]
        public string Order_Date { get; set; }

        [Display(Name = "Quoted Price")]
        public float Quoted_Price { get; set; }

        [Display(Name = "Amount Due")]
        public float Amount_Due { get; set; }

        [Display(Name = "Status ID")]
        public int StatusId { get; set; }

        [Display(Name = "Client ID")]
        [Required(ErrorMessage = "Client ID is required")]
        public int ClientId { get; set; }

        [Display(Name = "Data Report")]
        public string Data_Report_URL { get; set; }

        [Display(Name = "Summary Report")]
        public string Summary_Report_URL { get; set; }

        [Display(Name = "Discount")]
        public float Discount_Percent { get; set; }
    }
}