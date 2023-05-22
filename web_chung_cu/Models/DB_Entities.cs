using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace web_chung_cu.Models
{
    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("web_chung_cu") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Apartment>()
            .ToTable("Apartments")
            .HasRequired(a => a.user)
            .WithMany(u => u.apartments)
            .HasForeignKey(a => a.userId);

            modelBuilder.Entity<Room>()
            .ToTable("Rooms")
            .HasRequired(r => r.apartment)
            .WithMany(a => a.rooms)
            .HasForeignKey(r => r.apartmentId);

            modelBuilder.Entity<RoomImage>()
            .ToTable("RoomImages")
            .HasRequired(ri => ri.room)
            .WithMany(r => r.roomImages)
            .HasForeignKey(ri => ri.roomId);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}