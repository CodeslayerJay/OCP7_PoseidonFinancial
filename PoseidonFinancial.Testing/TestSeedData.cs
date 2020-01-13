using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.AppUtilities;

namespace PoseidonFinancial.Testing
{
    internal class TestSeedData
    {
        public const string TestUsername = "unitTester";

        internal static void Initialize(LocalDbContext db)
        {
            if(db != null)
            {
                var context = db;

                var user = context.Users.Where(x => x.UserName == TestUsername).SingleOrDefault();

                if (user == null)
                {
                    user = new User
                    {
                        FullName = "Unit Tester",
                        UserName = TestUsername,
                        Password = AppSecurity.HashPassword("test1234!").HashedPassword,
                        Role = "Admin"
                    };

                    context.Users.Add(user);
                }


                // Add additional dummy user for when testing user deletions
                var dummyUser = context.Users.Where(x => x.UserName == "dummyUser").SingleOrDefault();

                if(dummyUser == null)
                {
                    var dummy = new User
                    {
                        FullName = "Dummy User",
                        UserName = "dummy",
                        Password = AppSecurity.HashPassword("test1234!").HashedPassword,
                        Role = "Tester"
                    };

                    context.Users.Add(dummy);
                }


                context.BidList.AddRange(
                      new BidList
                      {
                          Account = user.UserName,
                          Type = "A simple bid",
                          BidQuantity = 10
                      },
                      new BidList
                      {
                          Account = user.UserName,
                          Type = "Another bid",
                          BidQuantity = 5
                      },
                      new BidList
                      {
                          Account = user.UserName,
                          Type = "Bid away!!",
                          BidQuantity = 50
                      }
                  );

                context.CurvePoints.AddRange(
                    new CurvePoint
                    {
                        CurveId = 1,
                        Term = 3,
                        Value = 1
                    },
                    new CurvePoint
                    {
                        CurveId = 1,
                        Term = 3,
                        Value = 5
                    },
                    new CurvePoint
                    {
                        CurveId = 1,
                        Term = 3,
                        Value = 5
                    },
                    new CurvePoint
                    {
                        CurveId = 1,
                        Term = 3,
                        Value = 5
                    }
                );

                context.Ratings.AddRange(
                        new Rating
                        {
                            MoodysRating = "asd",
                            SandPRating = "Yuasdp",
                            FitchRating = "Hm123123mm"
                        },
                         new Rating
                         {
                             MoodysRating = "Blsdah",
                             SandPRating = "Ydup",
                             FitchRating = "Hmsdamm"
                         },
                          new Rating
                          {
                              MoodysRating = "Blaaah",
                              SandPRating = "Yuaap",
                              FitchRating = "Hmdddmm"
                          },
                          new Rating
                          {
                              MoodysRating = "Blah",
                              SandPRating = "Yup",
                              FitchRating = "Hmmm"
                          }
                    );

                context.RuleNames.AddRange(
                        new RuleName
                        {
                            Name = "This is a rule",
                            Description = "Test123",
                            Template = "ABC123"
                        },
                        new RuleName
                        {
                            Name = "This is a rule",
                            Description = "asdad",
                            Template = "ABC123"
                        },
                        new RuleName
                        {
                            Name = "Tasd",
                            Description = "czc",
                            Template = "ABC123"
                        }
                    );

                context.Trades.AddRange(
                        new Trade
                        {
                            Account = user.UserName,
                            Type = "Buy",
                            BuyQuantity = 5
                        },
                        new Trade
                        {
                            Account = user.UserName,
                            Type = "Buy",
                            BuyQuantity = 2
                        }
                    );

                context.SaveChanges();
            }
        }
    }
}