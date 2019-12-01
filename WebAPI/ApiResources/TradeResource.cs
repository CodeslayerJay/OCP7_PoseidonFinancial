using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ApiResources
{
    public class TradeResource
    {
        public int TradeId { get; set; }
        public string Account { get; set; }
        public string Type { get; set; }
        public double BuyQuantity { get; set; }
    }

    public class CreateTradeResource
    {
        public string Account { get; set; }
        public string Type { get; set; }
        public double BuyQuantity { get; set; }
    }

    public class EditTradeResource
    {
        public int TradeId { get; set; }
        public string Account { get; set; }
        public string Type { get; set; }
        public double BuyQuantity { get; set; }
    }
}
