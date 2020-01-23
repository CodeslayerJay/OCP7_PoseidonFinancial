using Dot.Net.WebApi;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebApi.AppUtilities;

namespace WebApi.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var services = serviceProvider;
            
            try
            {
                var dbOptions = services.GetRequiredService<DbContextOptions<LocalDbContext>>();

                using (var context = new LocalDbContext(dbOptions))
                {
                    context.Database.Migrate();

                    var user = context.Users.Where(x => x.UserName == "unitTester").SingleOrDefault();

                    if (user == null)
                    {
                        user = new User
                        {
                            FullName = "Unit Tester",
                            UserName = "unitTester",
                            Password = AppSecurity.HashPassword("test1234!").HashedPassword,
                            Role = "Admin"
                        };

                        context.Users.Add(user);
                    }

                    if (!context.BidList.Any())
                    {
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
                    }

                    if (!context.CurvePoints.Any())
                    {
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
                    }

                    if (!context.Ratings.Any())
                    {
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
                    }

                    if (!context.RuleNames.Any())
                    {
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
                    }

                    if (!context.Trades.Any())
                    {
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
                    }

                    context.SaveChanges();
                }
                
            }
            catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<SeedData>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}