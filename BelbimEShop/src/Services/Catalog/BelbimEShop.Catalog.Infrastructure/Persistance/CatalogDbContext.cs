using BelbimEShop.Catalog.Domain.Aggregates;
using BelbimEShop.Shared.Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelbimEShop.Catalog.Infrastructure.Persistance
{
    public class CatalogDbContext : DbContext
    {

        private readonly IMediator mediator;

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey(p => p.CategoryId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>().HasData(
                new Category(1, "Elektronik", "Her türlü ev elektroniği"),
                new Category(2, "Kitap", "Her türlü kitap")
                );

            modelBuilder.Entity<Product>().HasData(
                new Product(1L, "Televizyon", "4K Ultra HD Smart TV", 4999.99m, 1, null, 1),
                new Product(2, "Laptop", "Yüksek performanslı dizüstü bilgisayar", 7999.99m, 1, null, 1),
                new Product(3, "Zihnin analizi", "Bertrand Russel", 100m, 1, null, 2),
                new Product(4, "Varlık ve Hiçlik", "Jean Paul Sartre", 150m, 1, null, 2)

                );



        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //DomainEvent'ler burada publish edilecek...

            var domainEvents = ChangeTracker.Entries<IAggregateRoot>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any())
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            var output = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }

            return output;

        }
    }
}
