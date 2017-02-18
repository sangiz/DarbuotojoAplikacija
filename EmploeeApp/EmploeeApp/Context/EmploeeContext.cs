using EmploeeApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmploeeApp.Context
{
    public class EmploeeContext : DbContext
    {
        public DbSet<Emploee> Employees { get; set; }
    }
}