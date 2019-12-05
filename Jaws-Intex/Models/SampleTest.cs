using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Sample_Test")]
    public class SampleTest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Sample")]
        public int SampleId { get; set; }
        public virtual Sample Sample { get; set; }

        [Required]
        [ForeignKey("Test")]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public string Notes { get; set; }

        public Boolean Active { get; set; }

    }
}