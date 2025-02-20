using loja_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace loja_api.Context;

public class ContextDB : DbContext
{

    public ContextDB(DbContextOptions<ContextDB> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Cupom> Cupom { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Storage> Storage { get; set; }
    public DbSet<MarketCart> MarketCart { get; set; }

    //para fazer a chamada do banco de dados

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cupom>()
            .OwnsOne(c => c.Auditable);

        // Configuração da entidade Employee com Auditable
        modelBuilder.Entity<Employee>()
            .OwnsOne(e => e.Auditable);

        // Configuração da relação entre MarketCart e User
        modelBuilder.Entity<MarketCart>()
            .HasOne(m => m.User)
            .WithMany(u => u.MarketCart)
            .HasForeignKey(m => m.MarketCartId);

        // Configuração da relação entre MarketCart e Cupom
        modelBuilder.Entity<MarketCart>()
            .HasOne(m => m.Cupom)
            .WithOne()
            .HasForeignKey<MarketCart>(m => m.CupomId);


        // Configuração de Products com Auditable
        modelBuilder.Entity<Products>()
            .OwnsOne(m => m.Auditable);

        // Configuração de Storage e Products
        modelBuilder.Entity<Storage>()
            .HasOne(s => s.Products)
            .WithMany(p => p.Storages)
            .HasForeignKey(s => s.IdProducts);

        // Configuração de Auditable no Storage
        modelBuilder.Entity<Storage>()
            .OwnsOne(s => s.Auditable);

        base.OnModelCreating(modelBuilder);
    }


}
