using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    public class Test
    {
        [Key]
        [Display(Name = "Test Id")]
        public int TestId { get; set; }

        [Required]
        [Display(Name = "Test Name")]
        public string TestName { get; set; }

        public string Protocol { get; set; }

        public int TestDurationDays { get; set; }

        public Boolean ConditionalTest { get; set; }

        public decimal BasePrice { get; set; }


    }
}