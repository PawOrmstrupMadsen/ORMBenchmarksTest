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
    public class EntityFrameworkAsyncDTO : ITestSignature
    {
        public async Task<long> GetPlayerByID(int id)
        {
            
                Stopwatch watch = new Stopwatch();
                watch.Start();
                using (SportContext context = new SportContext())
                {
                    var player = await context.Players.Select(p => new PlayerDTO()
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        DateOfBirth = p.DateOfBirth,
                        LastName = p.LastName,
                        TeamId = p.TeamId

                    }).FirstOrDefaultAsync(x => x.Id == id);
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
                    var players = await context.Players.Where(x => x.TeamId == teamId).Select(p => new PlayerDTO()
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        DateOfBirth = p.DateOfBirth,
                        LastName = p.LastName,
                        TeamId = p.TeamId
                    }).ToListAsync();
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
                    var players = await context.Teams.Where(x => x.SportId == sportId).Select(t => new TeamDTO()
                    {
                        Id = t.Id,
                        FoundingDate = t.FoundingDate,
                        Name = t.Name,
                        SportId = t.SportId,
                        Players = t.Players.Select(p => new PlayerDTO()
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            DateOfBirth = p.DateOfBirth,
                            LastName = p.LastName,
                            TeamId = p.TeamId
                        }).ToList()
                    }).ToListAsync();
                }
                watch.Stop();
                return watch.ElapsedMilliseconds;
           

        }
    }
}
