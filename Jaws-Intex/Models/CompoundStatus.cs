using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Compound_Status")]
    public class CompoundStatus
    {
        [Key]
        public int Id { get; set; }
        public int CompoundId { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [Display(Name = "Status Date")]
        public DateTime StatusDate { get; set; }
        public string StatusName { get; set; }
    }
}