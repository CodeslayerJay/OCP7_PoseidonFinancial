using AutoMapper;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;

namespace WebApi.AppUtilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Bidlist -> BidResource
            CreateMap<BidResource, BidList>();
            CreateMap<BidList, BidResource>();
            CreateMap<EditBidResource, BidList>();

            // CurvePoints
            CreateMap<CurveResource, CurvePoint>();
            CreateMap<CurvePoint, CurveResource>();
            CreateMap<EditCurveResource, CurvePoint>();

            // Rating
            CreateMap<RatingResource, Rating>();
            CreateMap<Rating, RatingResource>();
            CreateMap<EditRatingResource, Rating>();

            // User
            CreateMap<UserResource, User>();
            CreateMap<User, UserResource>();
            CreateMap<EditUserResource, User>();
            

            // Rules aka RuleName
            CreateMap<RuleNameResource, RuleName>();
            CreateMap<RuleName, RuleNameResource>();
            CreateMap<EditRuleNameResource, RuleName>();

            // Trades
            CreateMap<TradeResource, Trade>();
            CreateMap<Trade, TradeResource>();
            CreateMap<EditTradeResource, Trade>();

        }
    }
}
