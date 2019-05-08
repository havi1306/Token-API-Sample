using Code_Sample.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code_Sample.Helpers
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
