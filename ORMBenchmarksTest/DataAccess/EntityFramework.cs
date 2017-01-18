using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ORMBenchmarksTest.DataAccess
{
    public class EntityFramework : ITestSignature
    {
        public async Task<long> GetPlayerByID(int id)
        {
            return await Task.Run(() =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                using (SportContext context = new SportContext())
                {
                    var player = context.Players.FirstOrDefault(x => x.Id == id);
                }
                watch.Stop();
                return watch.ElapsedMilliseconds;
            });

        }

        public async Task<long> GetPlayersForTeam(int teamId)
        {
            return await Task.Run(() =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                using (SportContext context = new SportContext())
                {
                    var players = context.Players.AsNoTracking().Where(x => x.TeamId == teamId).ToList();
                }
                watch.Stop();
                return watch.ElapsedMilliseconds;
            });

        }

        public async Task<long> GetTeamsForSport(int sportId)
        {
            return await Task.Run(() =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                using (SportContext context = new SportContext())
                {
                    var players =
                        context.Teams.AsNoTracking().Include(x => x.Players).Where(x => x.SportId == sportId).ToList();
                }
                watch.Stop();
                return watch.ElapsedMilliseconds;
            });

        }
    }
}
