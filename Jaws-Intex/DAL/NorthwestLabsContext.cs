using Jaws_Intex.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jaws_Intex.DAL
{
    public class NorthwestLabsContext : DbContext
    {
        public NorthwestLabsContext() : base("NorthwestLabsContext") { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<Sample> Samples { get; set; }

    }
}