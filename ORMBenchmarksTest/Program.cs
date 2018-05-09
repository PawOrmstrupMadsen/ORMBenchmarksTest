using ORMBenchmarksTest.Configuration;
using ORMBenchmarksTest.DataAccess;
using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ORMBenchmarksTest
{
    class Program
    {
        private static Task _workingTask;
        public static int NumKids { get; set; }
        public static int NumPlayers { get; set; }
        public static int NumTeams { get; set; }
        public static int NumSports { get; set; }
        public static int NumRuns { get; set; }
        static void Main(string[] args)
        {
            MainAsync(args);
            while (!_workingTask.IsCompleted)
            {

            }
        }

        private static async void MainAsync(string[] args)
        {


            _workingTask = DoWork();
            await _workingTask;

        }

        static async Task DoWork()
        {
            char input;
            AutoMapperConfiguration.Configure();
            do
            {
                ShowMenu();

                input = Console.ReadLine().First();
                switch (input)
                {
                    case 'Q':
                        break;

                    case 'T':
                        List<TestResult> testResults = new List<TestResult>();
                        
                        Console.WriteLine("# of Test Runs:");
                        NumRuns = int.Parse(Console.ReadLine());

                        //Gather Details for Test
                        Console.WriteLine("# of Sports per Run: ");
                        NumSports = int.Parse(Console.ReadLine());

                        Console.WriteLine("# of Teams per Sport: ");
                        NumTeams = int.Parse(Console.ReadLine());

                        Console.WriteLine("# of Players per Team: ");
                        NumPlayers = int.Parse(Console.ReadLine());

                        Console.WriteLine("# of kids per Player: ");
                        NumKids = int.Parse(Console.ReadLine());


                        List<SportDTO> sports = Generator.GenerateSports(NumSports);
                        List<TeamDTO> teams = new List<TeamDTO>();
                        List<PlayerDTO> players = new List<PlayerDTO>();
                        List<KidDTO> kids = new List<KidDTO>();
                        foreach (var sport in sports)
                        {
                            var newTeams = Generator.GenerateTeams(sport.Id, NumTeams);
                            teams.AddRange(newTeams);
                            foreach (var team in newTeams)
                            {
                                var newPlayers = Generator.GeneratePlayers(team.Id, NumPlayers);
                                players.AddRange(newPlayers);
                                foreach (var player in newPlayers)
                                {
                                    var newKids = Generator.GenerateKids(player.Id, NumKids);
                                    kids.AddRange(newKids);
                                }
                            }
                        }

                        Database.Reset();
                        Database.Load(sports, teams, players, kids);

                        for (int i = 0; i < NumRuns; i++)
                        {

                            //EntityFrameworkDTO efTestDTO = new EntityFrameworkDTO();
                            //testResults.AddRange(await RunTests(i, Framework.EntityFrameworkDTO, efTestDTO));

                            EntityFramework efTest = new EntityFramework();
                            testResults.AddRange(await RunTests(i, Framework.EntityFramework, efTest));

                         
                        }
                        ProcessResults(testResults);

                        break;
                }

            }
            while (input != 'Q');
        }


        public static async Task<List<TestResult>> RunTests(int runID, Framework framework, ITestSignature testSignature)
        {
            List<TestResult> results = new List<TestResult>();

            TestResult result = new TestResult() { Run = runID, Framework = framework };
            List<long> playerByIDResults = new List<long>();
            for (int i = 1; i <= NumPlayers; i++)
            {
                playerByIDResults.Add(await testSignature.GetPlayerByID(i));
            }
            result.PlayerByIDMilliseconds = Math.Round(playerByIDResults.Average(), 4);

            List<long> playersForTeamResults = new List<long>();
            for (int i = 1; i <= NumTeams; i++)
            {
                playersForTeamResults.Add(await testSignature.GetPlayersForTeam(i));
            }
            result.PlayersForTeamMilliseconds = Math.Round(playersForTeamResults.Average(), 4);
            List<long> teamsForSportResults = new List<long>();
            for (int i = 1; i <= NumSports; i++)
            {
                teamsForSportResults.Add(await testSignature.GetTeamsForSport(i));
            }
            result.TeamsForSportMilliseconds = Math.Round(teamsForSportResults.Average(), 4);
            results.Add(result);

            return results;
        }

        public static void ProcessResults(List<TestResult> results)
        {
            var groupedResults = results.GroupBy(x => x.Framework);
            foreach(var group in groupedResults)
            {
                Console.WriteLine(group.Key.ToString() + " Results");
                Console.WriteLine("Run #\tPlayer by ID\t\tPlayers per Team\t\tTeams per Sport");
                var orderedResults = group.OrderBy(x=>x.Run);
                foreach(var orderResult in orderedResults)
                {
                    Console.WriteLine(orderResult.Run.ToString() + "\t\t" + orderResult.PlayerByIDMilliseconds + "\t\t\t" + orderResult.PlayersForTeamMilliseconds + "\t\t\t" + orderResult.TeamsForSportMilliseconds);
                }
                //Console.WriteLine($"Total\t\t{orderedResults.Average(x => x.PlayerByIDMilliseconds)} ms\t\t\t{orderedResults.Average(x => x.PlayersForTeamMilliseconds)} ms\t\t\t{orderedResults.Average(x => x.TeamsForSportMilliseconds)} ms");
            }
        }

        public static void ShowMenu()
        {
            Console.WriteLine("Please enter one of the following options:");
            Console.WriteLine("Q - Quit");
            Console.WriteLine("T - Run Test");
            Console.WriteLine("Option:");
        }
    }
}
