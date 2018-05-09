using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace ORMBenchmarksTest.TestData
{
    public static class Database
    {
        public static void Reset()
        {
            using(SportContext context = new SportContext())
            {
                context.Database.Initialize(true);
              
            }
        }

        public static void Load(List<SportDTO> sports, List<TeamDTO> teams, List<PlayerDTO> players, List<KidDTO> kids)
        {
            AddSports(sports);
            AddTeams(teams);
            AddPlayers(players);
            AddKids(kids);
        }
        private static void AddKids(List<KidDTO> kids)
        {
            using (SportContext context = new SportContext())
            {
                var list = new List<Kid>(kids.Count);

                foreach (var kid in kids)
                {
                    list.Add(new Kid()
                    {
                        FirstName = kid.FirstName,
                        LastName = kid.LastName,
                        DateOfBirth = kid.DateOfBirth,
                        PlayerId = kid.PlayerId,
                        Id = kid.Id,
                        
                    });
                }
                context.Kids.AddRange(list);
                context.SaveChanges();
            }
        }
        private static void AddPlayers(List<PlayerDTO> players)
        {
            using (SportContext context = new SportContext())
            {

                var list = new List<Player>(players.Count);
                
                foreach (var player in players)
                {
                    var teamIds = player.Teams.Select(x => x.Id).ToList();
                    list.Add(new Player()
                    {
                        FirstName = player.FirstName,
                        LastName = player.LastName,
                        DateOfBirth = player.DateOfBirth,
                        Id = player.Id,
                        Teams = context.Teams.Where(y => teamIds.Contains(y.Id)).ToList()
                    });
                }
                context.Players.AddRange(list);
                context.SaveChanges();
            }
        }

        private static void AddTeams(List<TeamDTO> teams)
        {
            using (SportContext context = new SportContext())
            {
                var list = new List<Team>(teams.Count);
                foreach (var team in teams)
                {
                    list.Add(new Team()
                    {
                        Name = team.Name,
                        Id = team.Id,
                        SportId = team.SportId,
                        FoundingDate = team.FoundingDate
                    });
                }
                context.Teams.AddRange(list);
                context.SaveChanges();
            }
        }

        private static void AddSports(List<SportDTO> sports)
        {
            using (SportContext context = new SportContext())
            {
                var list = new List<Sport>(sports.Count);
                foreach (var sport in sports)
                {
                    list.Add(new Sport()
                    {
                        Id = sport.Id,
                        Name = sport.Name
                    });
                }
                context.Sports.AddRange(list);
                context.SaveChanges();
            }
        }
    }
}
