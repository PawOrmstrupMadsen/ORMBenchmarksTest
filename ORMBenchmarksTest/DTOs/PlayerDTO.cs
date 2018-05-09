using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.DTOs
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        public ICollection<KidDTO> Kids { get; set; } = new List<KidDTO>();

    }
}
