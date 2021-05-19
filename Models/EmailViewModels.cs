using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BillTracker.Models
{
    public class Emails
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(150)]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public bool GoogleTrends { get; set; }
    }

    public class EmailViewModels
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public bool GoogleTrends { get; set; }
    }
}