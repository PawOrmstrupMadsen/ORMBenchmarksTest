using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMBenchmarksTest.Models
{
    public class Player
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
        public virtual ICollection<Kid> Kids { get; set; } = new HashSet<Kid>();
    }
}
