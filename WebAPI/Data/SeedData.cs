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
                    context.Database.EnsureCreated();

                    // Let's remove this record if we currently have it so we can test creating the user
                    // as unique
                    var tempUser = context.Users.Where(x => x.UserName == "tempUsername").SingleOrDefault();

                    if (tempUser != null)
                    {
                        context.Users.Remove(tempUser);
                    }


                    // Add additional dummy user for when testing user deletions
                    var dummyUser = context.Users.Where(x => x.UserName == "dummy").SingleOrDefault();

                    if (dummyUser == null)
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

                    var admin = context.Users.Where(x => x.UserName == "admin").SingleOrDefault();

                    if (admin == null)
                    {
                        user = new User
                        {
                            FullName = "App Admin",
                            UserName = "admin",
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