﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoWD.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LoWDEntities : DbContext
    {
        public LoWDEntities()
            : base("name=LoWDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<game_played> game_played { get; set; }
        public virtual DbSet<lord> lords { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<game> games { get; set; }
    }
}
