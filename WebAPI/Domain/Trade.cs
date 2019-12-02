using System;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class Trade
    {
        [Key]
        public int TradeId { get; set; }

        [StringLength(20)]
        public string Account { get; set; }

        [StringLength(20)]
        public string Type { get; set; }
        public double BuyQuantity { get; set; }
        public double SellQuantity { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }

        [StringLength(20)]
        public string Benchmark { get; set; }
        public DateTime TradeDate { get; set; } = DateTime.Now;

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
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string RevisionName { get; set; }
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