using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.DataAccess
{
    public interface ITestSignature
    {
        Task<long> GetPlayerByID(int id);
        Task<long> GetPlayersForTeam(int teamID);
        Task<long> GetTeamsForSport(int sportID);
    }
}
