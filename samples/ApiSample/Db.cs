using ApiSample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSample
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<ResetPasswordToken>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            });
        }
    }
}
