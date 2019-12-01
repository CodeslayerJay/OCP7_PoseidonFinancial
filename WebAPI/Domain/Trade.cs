using System;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class Trade
    {
        
        public int TradeId { get; set; }

        
        public string Account { get; set; }
        
        public string Type { get; set; }
        public double BuyQuantity { get; set; }
        public double SellQuantity { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public string Benchmark { get; set; }
        public DateTime TradeDate { get; set; } = DateTime.Now;
        public string Security { get; set; }
        public string Status { get; set; }
        public string Trader { get; set; }
        public string Book { get; set; }
        public string CreationName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string RevisionName { get; set; }
        public DateTime RevisionDate { get; set; } = DateTime.Now;
        public string DealName { get; set; }
        public string DealType { get; set; }
        public string SourceListId { get; set; }
        public string Side { get; set; }
    }
}