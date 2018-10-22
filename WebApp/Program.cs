using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace WebApp
{
    public class Program
    {
        private static SamuraiDbContext _context = new SamuraiDbContext();

        public static void Main(string[] args)
        {
            //InsertSamurais();
            //InsertMultipleSamurais();

            //Lista todos samurais.
            MoreQueries();
            //RetornaTodosSamurais();

            //Insert into Battles
            InsertBattle();

            CreateWebHostBuilder(args).Build().Run();

        }
        //Insert Battles
        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Ozzy",
                StartDate = new DateTime(1979, 01, 01),
                EndDate = new DateTime(1980, 01, 31)
            });
            _context.SaveChanges();
           
        }

        //update Battles

        public static void UpdateQueryBattle_Disconnected()
        {
            var obj = _context.Battles.FirstOrDefault();
            obj.EndDate = new DateTime(2000, 01, 01);

            using (var NewContext = new SamuraiDbContext())
            {
                NewContext.Battles.Update(obj);
                NewContext.SaveChanges();
            }

        }


        //Update
        private static void UpdateQuery()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "Everton";
            _context.SaveChanges();
        }

        //Update
        private static void UpdateAllName()
        {
            var samurai = _context.Samurais.ToList();
            samurai.ForEach(s => s.Name += "W");
            _context.SaveChanges();
        }

        private static void MoreQueries()
        {
            var name = "Everton";
            var samuraiObj = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "E%")).ToList();
            //var samurais = _context.Samurais.Where(s => s.Name == "Everton").ToList();  
            //var lastName = _context.Samurais.OrderBy(s => s.Id).LastOrDefault(s => s.Name == name);
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "MR. M" };
            var battle = new Battle
            {
                Name = "Senhor dos Aneis",
                StartDate = new DateTime(1800, 05, 25),
                EndDate = new DateTime(1900, 05, 01)
            };

            using (var context = new SamuraiDbContext())
            {
                context.AddRange(samurai, battle);
                context.SaveChanges();
            }
        }

        private static void InsertSamurais()
        {
            var samurai = new Samurai { Name = "Everton mota" };
            var samuraiM = new Samurai { Name = "Mr Samurai" };

            using (var context = new SamuraiDbContext())
            {
                context.Samurais.AddRange(samurai, samuraiM);
                
                context.SaveChanges();
            }
        }

        private static void RetornaTodosSamurais()
        {
            using( var context = new SamuraiDbContext())
            {
                var samurais = context.Samurais.ToList();
                var query = context.Samurais;
                foreach (var s in samurais )
                {
                    Console.WriteLine(s.Name);
                }
            }

        }

        //delete
        private static void DeleteWhileNotTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Everton Mota");
            using (var ContextApp = new SamuraiDbContext())
            {
                ContextApp.Samurais.Remove(samurai);
                ContextApp.SaveChanges();
            }
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
