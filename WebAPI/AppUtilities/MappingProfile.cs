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

            // CurvePoints
            CreateMap<CurveResource, CurvePoint>();
            CreateMap<CurvePoint, CurveResource>();

            // Rating
            CreateMap<RatingResource, Rating>();
            CreateMap<Rating, RatingResource>();

            // User
            CreateMap<UserResource, User>();
            CreateMap<User, UserResource>();
            CreateMap<CreateUserResource, User>();
        }
    }
}
