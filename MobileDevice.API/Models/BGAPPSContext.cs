using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MobileDevice.API.Models
{
    public partial class BGAPPSContext : DbContext
    {
        public BGAPPSContext()
        {
        }

        public BGAPPSContext(DbContextOptions<BGAPPSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MdaAppUser> MdaAppUser { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlServer("Server=BGDNDV;Database=BGAPPS;Integrated Security=true;");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "BG\\bwalrod");


        }
    }
}
