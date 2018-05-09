using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMBenchmarksTest.Models
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FoundingDate { get; set; }
        public int SportId { get; set; }
        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();
        public virtual Sport Sport { get; set; }
    }
}
