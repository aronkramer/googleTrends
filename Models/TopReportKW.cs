using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BillTracker.Models
{
    public class TopReportKW
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        [Required]
        public string Sku { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        [Required]
        public string SearchTerm { get; set; }
        public decimal? Sales { get; set; }
    }

    public class TopReportKWViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string SearchTerm { get; set; }
        public decimal Sales { get; set; }
    }
}