using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ships
{
    public partial class ShipsContext : DbContext
    {
        public ShipsContext()
        {
        }

        public ShipsContext(DbContextOptions<ShipsContext> options)
            : base(options)
        {
        }

        private const string strConnection = "Data Source=204-1;Initial Catalog=Ships;Integrated Security=True";

        public virtual DbSet<Battle> Battles { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Outcome> Outcomes { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }

        //1
        public void CreateClassOfShip(string @class, string type, string country, byte numGuns, int bore, int displacement)
        {
            using (var entity = new ShipsContext())
            {
                entity.Classes.Add(new Class(@class, type, country, numGuns, bore, displacement));
                //entity.SaveChanges();
            }
        }

        //2
        public void CreateShip(string name, string @class, short launched, string type, string country, byte numGuns, int bore, int displacement)
        {
            using (var entity = new ShipsContext())
            {
                entity.Ships.Add(new Ship(name, @class, launched));
                CreateClassOfShip(@class, type, country, numGuns, bore, displacement);
                //entity.SaveChanges();
            }
        }

        //3
        public void CreateBattle(string name, DateTime date, string ship, string result)
        {
            using (var entity = new ShipsContext())
            {
                entity.Battles.Add(new Battle(name, date));
                entity.Outcomes.Add(new Outcome(ship, name, result));
                //entity.SaveChanges();
            }
        }

        //4
        public void ShowShips()
        {
            using (var entity = new ShipsContext())
            {
                foreach (var item in entity.Ships)
                {
                    Console.WriteLine($"Name: {item.Name}");
                }
            }
        }

        //5
        public void ShowResultOfBattleByName(string value)
        {
            using (var entity = new ShipsContext())
            {
                foreach (var item in entity.Outcomes)
                {
                    if (item.Ship == value)
                    {
                        Console.WriteLine($"Result: {item.Result}\n");
                    }
                }
            }
        }

        //6
        public void ShowBattleByName(string value)
        {
            using (var entity = new ShipsContext())
            {
                foreach (var item in entity.Outcomes.Include(x=>x.BattleNavigation))
                {
                    if (item.BattleNavigation.Name == value)
                    {
                        Console.WriteLine($"Battle: {item.Battle}\nDate: {item.BattleNavigation.Date}\nShip: {item.Ship}\nResult: {item.Result}\n");
                    }
                }
            }
        }

        //7
        public void ShowAllBattlesByDate(DateTime dateTime)
        {
            using (var entity = new ShipsContext())
            {
                foreach (var item in entity.Battles)
                {
                    if (item.Date == dateTime)
                    {
                        Console.WriteLine($"Battle: {item.Name}");
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(strConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Battle>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Class1);

                entity.Property(e => e.Class1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class");

                entity.Property(e => e.Bore).HasColumnName("bore");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Displacement).HasColumnName("displacement");

                entity.Property(e => e.NumGuns).HasColumnName("numGuns");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Outcome>(entity =>
            {
                entity.HasKey(e => new { e.Ship, e.Battle });

                entity.Property(e => e.Ship)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ship");

                entity.Property(e => e.Battle)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("battle");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("result");

                entity.HasOne(d => d.BattleNavigation)
                    .WithMany(p => p.Outcomes)
                    .HasForeignKey(d => d.Battle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Outcomes_Battles");
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class");

                entity.Property(e => e.Launched).HasColumnName("launched");

                entity.HasOne(d => d.ClassNavigation)
                    .WithMany(p => p.Ships)
                    .HasForeignKey(d => d.Class)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ships_Classes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
