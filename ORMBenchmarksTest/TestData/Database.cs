using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System.Collections.Generic;

namespace ORMBenchmarksTest.TestData
{
    public static class Database
    {
        public static void Reset()
        {
            using(SportContext context = new SportContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Player");
                context.Database.ExecuteSqlCommand("DELETE FROM Team");
                context.Database.ExecuteSqlCommand("DELETE FROM Sport");
            }
        }

        public static void Load(List<SportDTO> sports, List<TeamDTO> teams, List<PlayerDTO> players)
        {
            AddSports(sports);
            AddTeams(teams);
            AddPlayers(players);
        }

        private static void AddPlayers(List<PlayerDTO> players)
        {
            using (SportContext context = new SportContext())
            {
                var list = new List<Player>(players.Count);

                foreach (var player in players)
                {
                    list.Add(new Player()
                    {
                        FirstName = player.FirstName,
                        LastName = player.LastName,
                        DateOfBirth = player.DateOfBirth,
                        TeamId = player.TeamId,
                        Id = player.Id
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
