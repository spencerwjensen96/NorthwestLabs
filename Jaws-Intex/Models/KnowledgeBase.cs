using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jaws_Intex.Models
{
    [Table("Knowledge_Base")]
    public class KnowledgeBase
    {
        [Key]
        public int KnowledgeBaseId { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Author")]
        [Required]
        public string Author { get; set; }
        
        [Display(Name = "Date")]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Contents")]
        [Required]
        public string Contents { get; set; }
    }
}