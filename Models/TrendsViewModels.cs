using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BillTracker.Models
{
    public class Trends
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Keyword { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Date { get; set; }
        public int? Ranking { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool NoData { get; set; }
    }

    public class TrendsViewModels
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Date { get; set; }
        public int Ranking { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool NoData { get; set; }
    }

    
}