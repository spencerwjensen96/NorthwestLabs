using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Work_Order_Status")]
    public class WorkOrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
    }
}