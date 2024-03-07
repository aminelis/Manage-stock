using Microsoft.EntityFrameworkCore;
using db_Api.Models.Generated.Reunion;
using db_Api.Models.Generated.Employee;
using db_Api.Models.Generated.Connexion;
using db_Api.Models.Generated.Product;
namespace db_Api.Models.Generated
{
    public class Db_PartialContext : DbContext
    {
        public Db_PartialContext()
        {
        }

        public Db_PartialContext(DbContextOptions<Db_PartialContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OK> OKs { get; set; }
        public virtual DbSet<IsDone> IsDones { get; set; }
        public virtual DbSet<Delete> Deletes { get; set; }
        public virtual DbSet<ListeReunion11> ListeReunion11s { get; set; }
        public virtual DbSet<ListeEmployee> ListeEmployees { get; set; }
        public virtual DbSet<UserModel> users { get; set; }
        public virtual DbSet<ProduitModel> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Delete>(entity =>
                {
                    entity.HasKey(e => e.deleted);
                    entity.ToTable("Delete");
                });
                modelBuilder.Entity<IsDone>(entity =>
                {
                    entity.HasKey(e => e.done);
                    entity.ToTable("IsDone");
                });
                modelBuilder.Entity<OK>(entity =>
                {
                    entity.HasKey(e => e.ok);
                    entity.ToTable("OK");
                });


                                modelBuilder.Entity<ListeReunion11>(entity =>
                {
                    entity.HasKey(e => e.NReunion);
                    entity.ToTable("ListeReunion11");
                    entity.Property(e => e.TypeReunion).HasColumnName("TypeR");
                    entity.Property(e => e.DatePrev).HasColumnName("DatePrev");
                    entity.Property(e => e.DateReal).HasColumnName("DateReal");
                    entity.Property(e => e.Lieu).HasColumnName("lieu");
                    entity.Property(e => e.Site).HasColumnName("site");

                    //entity.Property(e => e.Etat).HasColumnName("Etat");
                    //entity.Property(e => e.Lieu).HasColumnName("Lieu");
                    //entity.Property(e => e.Site).HasColumnName("Site");
                    //entity.Property(e => e.ordreJour).HasColumnName("ordreJour");
                    //entity.Property(e => e.StrEtat).HasColumnName("StrEtat");

                    //entity.Property(e => e.reunion_plus0).HasColumnName("reunion_plus0");
                    //entity.Property(e => e.reunion_plus1).HasColumnName("reunion_plus1");
                });

                             modelBuilder.Entity<ListeEmployee>(entity =>
                {
                    entity.HasKey(e => e.id);
                    entity.ToTable("employee");
                    entity.Property(e => e.name).HasColumnName("name");
                    entity.Property(e => e.email).HasColumnName("email");
                    entity.Property(e => e.salary).HasColumnName("salary");
                    entity.Property(e => e.address).HasColumnName("address");
                });

                          modelBuilder.Entity<UserModel>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.ToTable("users");
                    entity.Property(e => e.Username).HasColumnName("Username");
                    entity.Property(e => e.Password).HasColumnName("Password");
                    entity.Property(e => e.Role).HasColumnName("Role");
                });

                       modelBuilder.Entity<ProduitModel>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.ToTable("Product");
                    entity.Property(e => e.CodePdt).HasColumnName("CodePdt");
                    entity.Property(e => e.Produit).HasColumnName("Produit");
                    entity.Property(e => e.Prix).HasColumnName("Prix");
                });

            }
        }
    }

