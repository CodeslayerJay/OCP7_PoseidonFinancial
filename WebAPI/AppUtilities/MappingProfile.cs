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
            CreateMap<CreateBidListResource, BidList>();
            CreateMap<EditBidListResource, BidList>();

            // CurvePoints
            CreateMap<CurveResource, CurvePoint>();
            CreateMap<CurvePoint, CurveResource>();
            CreateMap<CreateCurvePointResource, CurvePoint>();
            CreateMap<EditCurvePointResource, CurvePoint>();

            // Rating
            CreateMap<RatingResource, Rating>();
            CreateMap<Rating, RatingResource>();
            CreateMap<CreateRatingResource, Rating>();
            CreateMap<EditRatingResource, Rating>();

            // User
            CreateMap<UserResource, User>();
            CreateMap<User, UserResource>();
            CreateMap<EditUserResource, User>();
            CreateMap<CreateUserResource, User>();

            // Rules aka RuleName
            CreateMap<RuleNameResource, RuleName>();
            CreateMap<RuleName, RuleNameResource>();
            CreateMap<CreateRuleNameResource, RuleName>();
            CreateMap<EditRuleNameResource, RuleName>();

            // Trades
            CreateMap<TradeResource, Trade>();
            CreateMap<Trade, TradeResource>();
            CreateMap<CreateTradeResource, Trade>();
            CreateMap<EditTradeResource, Trade>();

        }
    }
}
