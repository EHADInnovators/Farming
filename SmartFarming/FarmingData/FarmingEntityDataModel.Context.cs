﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FarmingData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SQLDBVirtualFarmerAssitEntities : DbContext
    {
        public SQLDBVirtualFarmerAssitEntities()
            : base("name=SQLDBVirtualFarmerAssitEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CHILD_FARM> CHILD_FARM { get; set; }
        public virtual DbSet<MASTER_FARM> MASTER_FARM { get; set; }
        public virtual DbSet<SEARCH_OPTIONS> SEARCH_OPTIONS { get; set; }
        public virtual DbSet<SYMPTOM_RESLUTS> SYMPTOM_RESLUTS { get; set; }
        public virtual DbSet<SYMPTOM> SYMPTOMS { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }
    }
}
