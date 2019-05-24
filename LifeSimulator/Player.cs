using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulator
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SpendedTime { get; set; }

        public int Happiness { get; set; }
    }

    public class PlayerContext : DbContext
    {
        public PlayerContext() : base("DbConnection") { }

        public DbSet<Player> Players { get; set; }
    }
}
