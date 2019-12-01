using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public class BidListRepository : RepositoryBase<BidList>, IBidListRepository
    {
        
        public BidListRepository(LocalDbContext appDbContext) : base(appDbContext)
        { }

        public override void Update(int id, BidList bid)
        {
            var bidToUpdate = AppDbContext.BidList.Find(id);

            if (bid != null && bidToUpdate != null)
            {
                //bidToUpdate.Account = bid.Account;
                //bidToUpdate.Ask = bid.Ask;
                //bidToUpdate.AskQuantity = bid.AskQuantity;
                //bidToUpdate.Benchmark = bid.Benchmark;
                //bidToUpdate.Bid = bid.Bid;
                //bidToUpdate.BidListDate = bid.BidListDate;
                //bidToUpdate.BidQuantity = bid.BidQuantity;
                //bidToUpdate.Book = bid.Book;
                //bidToUpdate.Commentary = bid.Commentary;
                //bidToUpdate.CreationDate = bid.CreationDate;
                //bidToUpdate.CreationName = bid.CreationName;
                //bidToUpdate.DealName = bid.DealName;
                //bidToUpdate.DealType = bid.DealType;
                //bidToUpdate.RevisionDate = bid.RevisionDate;
                //bidToUpdate.RevisionName = bid.RevisionName;
                //bidToUpdate.Security = bid.Security;
                //bidToUpdate.Side = bid.Side;
                //bidToUpdate.SourceListId = bid.SourceListId;
                //bidToUpdate.Trader = bid.Trader;
                //bidToUpdate.Type = bid.Type;

                AppDbContext.BidList.Update(bidToUpdate);
            }
        }

    }
}
