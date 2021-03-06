﻿using Jaws_Intex.Models;
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
        public DbSet<WorkOrderStatus> WorkOrderStatuses { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<CompoundStatus> CompoundStatuses { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<SampleTest> SampleTests { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<KnowledgeBase> knowledgeBases { get; set; }
    }
}