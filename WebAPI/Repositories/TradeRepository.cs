using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public class TradeRepository : RepositoryBase<Trade>, ITradeRepository
    {

        public TradeRepository(LocalDbContext appDbContext) :base(appDbContext)
        {  }

     
        public override void Update(int id, Trade trade)
        {
            var tradeToUpdate = AppDbContext.Trades.Find(id);

            if (trade != null && tradeToUpdate != null)
            {
                tradeToUpdate.Account = trade.Account;
                tradeToUpdate.Benchmark = trade.Benchmark;
                tradeToUpdate.Book = trade.Book;
                tradeToUpdate.BuyPrice = trade.BuyPrice;
                tradeToUpdate.BuyQuantity = trade.BuyQuantity;
                tradeToUpdate.CreationName = trade.CreationName;
                tradeToUpdate.DealName = trade.DealName;
                tradeToUpdate.DealType = trade.DealType;
                tradeToUpdate.RevisionName = trade.RevisionName;
                tradeToUpdate.Security = trade.Security;
                tradeToUpdate.SellPrice = trade.SellPrice;
                tradeToUpdate.SellQuantity = trade.SellQuantity;
                tradeToUpdate.Side = trade.Side;
                tradeToUpdate.SourceListId = trade.SourceListId;
                tradeToUpdate.Status = trade.Status;
                tradeToUpdate.TradeDate = trade.TradeDate;
                tradeToUpdate.Trader = trade.Trader;
                tradeToUpdate.Type = trade.Type;

                AppDbContext.Trades.Update(tradeToUpdate);
            }
        }


    }
}
