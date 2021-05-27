using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BillTracker.Models
{
    public class SearchTermReport
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(250)]
        [Required]
        public string Campaign { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(250)]
        [Required]
        public string SearchTerm { get; set; }
        [Required]
        public decimal Sales { get; set; }
        [Required]
        public int Units { get; set; }
        public decimal? Acos { get; set; }
    }

    public class SearchTermReportViewModel
    {
        public string Campaign { get; set; }
        public string SearchTerm { get; set; }
        public decimal Sales { get; set; }
        public int Units { get; set; }
        public decimal? Acos { get; set; }
    }
}