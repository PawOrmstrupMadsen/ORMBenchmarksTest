using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ORMBenchmarksTest.DataAccess
{
    public class EntityFrameworkAsync : ITestSignature
    {
        public async Task<long> GetPlayerByID(int id)
        {
            
                Stopwatch watch = new Stopwatch();
                watch.Start();
                using (SportContext context = new SportContext())
                {
                    var player = await context.Players.FirstOrDefaultAsync(x => x.Id == id);
                }
                watch.Stop();
                return watch.ElapsedMilliseconds;
          

        }

        public async Task<long> GetPlayersForTeam(int teamId)
        {
            
                Stopwatch watch = new Stopwatch();
                watch.Start();
                using (SportContext context = new SportContext())
                {
                    var players = await context.Players.AsNoTracking().Where(x => x.TeamId == teamId).ToListAsync();
                }
                watch.Stop();
                return watch.ElapsedMilliseconds;
          

        }

        public async Task<long> GetTeamsForSport(int sportId)
        {
           
                Stopwatch watch = new Stopwatch();
                watch.Start();
                using (SportContext context = new SportContext())
                {
                    var players = await context.Teams.AsNoTracking().Include(x => x.Players).Where(x => x.SportId == sportId).ToListAsync();
                }
                watch.Stop();
                return watch.ElapsedMilliseconds;
           

        }
    }
}
