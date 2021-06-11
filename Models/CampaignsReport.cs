using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BillTracker.Models
{
    public class CampaignsReport
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(250)]
        [Required]
        public string Campaign { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(250)]
        public string Sku { get; set; }
    }

    public class CampaignsReportViewModel
    {
        public string Campaign { get; set; }
        public string Sku { get; set; }
    }
}