using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BillTracker.Models
{
    public class TopKeywords
    {   
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        [Required]
        public string SKU { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        public string Asin { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        [Required]
        public string Keyword { get; set; }
        public bool Deleted { get; set; }
    }

    public class TopKeywordViewModels
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Asin { get; set; }
        public string Keyword { get; set; }
    }
}