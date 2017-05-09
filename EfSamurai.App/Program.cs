using EfSamurai.Data;
using EfSamurai.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EfSamurai.App
{
    public class Program
    {
        static SamuraiContext context = new SamuraiContext();

        static void Main(string[] args)
        {
            AddOneSamurai();
            AddMultipleSamurais();

            AddSomeBattles();
            AddOneSamuraiWithRelatedData();
            
            ListAllSamuraiNames();
            ListAllSamuraiNames_OrderByName();
            ListAllSamuraiNames_OrderByIdDescending();
            //Console.WriteLine("Enter the real name of a samurai:");
            //FindSamuraiWithRealName(Console.ReadLine());
            
            //ListAllQuotesOfType(QuoteTypes.Awsume);
            ListAllQuotesOfType_WithSamurai(QuoteTypes.Awsume);

            ListAllBattles(new DateTime(1643,03,24), new DateTime(1643,03,26), true);
            ClearDatabase();
            Console.ReadLine();

        }
        public Program(SamuraiContext _context)
        {
            context = _context;


        }

        private static void AddOneSamurai()
        {
            var samuari = new Samurai { Name = "Zelda" };


            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samuari);
                context.SaveChanges();
            }
        }




        private static void AddMultipleSamurais()
        {
            var sams = new List<Samurai>
            {

            new Samurai { Name = "Zelda2" },
            new Samurai { Name = "Gregor" },
            new Samurai { Name = "Genji" }
             };


            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(sams);
                context.SaveChanges();
            }
        }

        static Battle GetBattle1()
        {
            return new Battle
            {
                Name = "Ninja War1",
                Brutal = true,
                StartDate = new DateTime(1643, 03, 23),
                EndDate = new DateTime(1644, 02, 23),

                BattleLog = new BattleLog()
                {
                    Name = "Ninja War1 Log",

                    BattleEvents = new List<BattleEvent>()
                        {
                            new BattleEvent(){ Description = "The Chef signaled to Attack", Summary = "The Cheif planned his sneaky attack that would overthrow the king" },
                            new BattleEvent(){ Description = "The King died", Summary = "This ended the war" }
                        }
                },

            };
        }

        private static void AddSomeBattles()
        {

            var battle = new List<Battle>
            {
                GetBattle1()


             };



            context.Battle.AddRange(battle);
            context.SaveChanges();

        }


        private static void AddOneSamuraiWithRelatedData()
        {
            var SamuraiWithData = new Samurai
            {
                Name = "Super Genji",
                Age = 23,
                HairStyle = HairStyles.Western,
                SamuraiBattles = new List<SamuraiBattle>
                {
                    new SamuraiBattle{ Battle =  GetBattle1()}
                },

                SecretIdentity = new SecretIdentity
                {
                    RealName = "Hans Flamenwavver"
                },
                Quotes = new List<Quotes>
                {
                    new Quotes { QuoteType = QuoteTypes.Awsume, SamuraiQuotes = "I can only fight when im drunk" }
                }

            };

            
            context.Samurais.Add(SamuraiWithData);
            context.SaveChanges();

        }

        private static void ClearDatabase()
        {
            //SqlCommand info = new SqlCommand();
            //info.Connection = new SqlConnection("Server = (localdb)\\mssqllocaldb; Database = EfSamurai; Trusted_Connection = True; ");
            //info.CommandType = CommandType.Text;
            //info.CommandText = "edit_.Clear()";
            //info.ExecuteNonQuery();

            context.Samurais.RemoveRange(context.Samurais);
            context.Battle.RemoveRange(context.Battle);
            context.SaveChanges();

            //context.Database.ExecuteSqlCommand("DELETE FROM Samurais");
            //context.Database.ExecuteSqlCommand("DELETE FROM Battle");
            //context.Database.ExecuteSqlCommand("DELETE FROM BattleEvent");
            //context.Database.ExecuteSqlCommand("DELETE FROM BattleLog");
            //context.Database.ExecuteSqlCommand("DELETE FROM Quotes");
            //context.Database.ExecuteSqlCommand("DELETE FROM SamuraiBattle");
            //context.Database.ExecuteSqlCommand("DELETE FROM SecretIdentity");
        }

        private static void ListAllSamuraiNames()
        {
            var AllSamurais = context.Samurais.Select(x => x.Name);
            foreach (var name in AllSamurais)

            {
                Console.WriteLine(name);
            }
            Console.WriteLine("");
        }

        private static void ListAllSamuraiNames_OrderByName()
        {
            var AllSamurais = context.Samurais.OrderBy(x => x.Name).Select(x => x.Name);
            foreach (var name in AllSamurais)

            {
                Console.WriteLine(name);
            }
            Console.WriteLine("");
        }

        private static void ListAllSamuraiNames_OrderByIdDescending()
        {
            var AllSamurais = context.Samurais.OrderByDescending(x => x.Id);
            foreach (var samurai in AllSamurais)

            {
                Console.WriteLine(samurai.Name +" "+ samurai.Id);
            }
           
            Console.WriteLine("");
        }

        private static void FindSamuraiWithRealName(string name)
        {
            var AllSamurais = context.SecretIdentity.FirstOrDefault(x => x.RealName == name);
            
            if (AllSamurais == null)
            {
                Console.WriteLine($"Samurai is a ghost, {name} doesn't \"exist\"");
            }
            else
            {
                Console.WriteLine($"The samurai {AllSamurais.RealName} exists, his nickname is {AllSamurais.Samurai.Name}");
            }

            Console.WriteLine("");
        }


        private static void ListAllQuotesOfType(QuoteTypes quoteTypes)
        {
            var findQuotesOfType = context.Quotes.Where(x => x.QuoteType == quoteTypes).Select(x=> x.SamuraiQuotes);

            foreach (var quote in findQuotesOfType)
            {
                Console.WriteLine($"This is an awsume quote: " +quote);
            }

        }


        private static void ListAllQuotesOfType_WithSamurai(QuoteTypes quoteTypes)
        {
            var findQuotesOfType = context.Quotes.Where(x => x.QuoteType == quoteTypes);

            

            foreach (var quote in findQuotesOfType)
            {
                Console.WriteLine($"This is an awsume quote: " + "\""+quote.SamuraiQuotes+"\"" + " by " + quote.Samurai.Name);
            }

        }

        private static void ListAllBattles(DateTime from, DateTime to, bool? isBrutal)
        {


            var battlesInTheIntervall = context.Battle.
                Where(x => x.StartDate <= from && x.EndDate >= to).ToList();


            if(battlesInTheIntervall.Count > 0)
            {
                foreach (var battle in battlesInTheIntervall)
                {
                    if (isBrutal == true)
                        Console.WriteLine($" {battle.Name} is a brutal battle within the period");
                    else if (isBrutal == false)
                        Console.WriteLine($" {battle.Name} is not a brutal battle within the period");
                    else
                        Console.WriteLine($" {battle.Name} is a battle within the period");
                }
            }
            
            else
                Console.WriteLine("Didnt find any battles within the intervall");
        }

    }

}



