using System.Data.Entity;
using ORMBenchmarksTest.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


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
                    var player = context.Players
                    .Include(x => x.Kids)
                    .Include(x => x.Teams)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id);
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
                    var players = context.Teams.Where(x => x.Id == teamId)
                        .SelectMany(x => x.Players)
                        .Include(x => x.Kids)
                        .Include(x => x.Teams)
                        .AsNoTracking()
                        .ToList();
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
                        context.Teams
                        .Include(x => x.Players.Select(y => y.Kids))
                        .AsNoTracking()
                        .Where(x => x.SportId == sportId).ToList();
                }
                watch.Stop();
                return watch.ElapsedMilliseconds;
            });

        }
    }
}
