using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Persistent
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext(string connectionString):base(connectionString)
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Element> Elements { get; set; }
    }
}
