﻿using System.Configuration;
using H5Forms.Entities.Interfaces;
using System.Data.Entity;

namespace H5Forms.EfRepository
{
    public class H5FormsContext : DbContext, IH5FormsContext
    {
        public H5FormsContext()
            : base(ConfigurationManager.ConnectionStrings["H5Forms"].ConnectionString)
        {
          
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public IDbSet<Entities.Form.Form> Forms{ get; set;}

        public IDbSet<Entities.Form.FormEntry> FormEntries { get; set; }

        public IDbSet<Entities.User> Users { get; set; }              
    }
}