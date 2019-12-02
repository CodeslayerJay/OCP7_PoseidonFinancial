using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    public class BidList
    {   
        [Key]
        public int BidListId { get; set; }
        
        [StringLength(20)]
        public string Account { get; set; }
        
        [StringLength(20)]
        public string Type { get; set; }
        
        public double BidQuantity { get; set; }
        public double AskQuantity { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        
        [StringLength(20)]
        public string Benchmark { get; set; }
        public DateTime BidListDate { get; set; } = DateTime.Now;
        
        [StringLength(20)]
        public string Commentary { get; set; }
        
        [StringLength(20)]
        public string Security { get; set; }
        
        [StringLength(20)]
        public string Status { get; set; }
        
        [StringLength(20)]
        public string Trader { get; set; }
        
        [StringLength(20)]
        public string Book { get; set; }
        
        [StringLength(20)]
        public string CreationName { get; set; }
        
        [StringLength(20)]
        public string RevisionName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime RevisionDate { get; set; } = DateTime.Now;
        
        [StringLength(20)]
        public string DealName { get; set; }
        
        [StringLength(20)]
        public string DealType { get; set; }
        [StringLength(20)]
        public string SourceListId { get; set; }
        [StringLength(20)]
        public string Side { get; set; }

    }
}