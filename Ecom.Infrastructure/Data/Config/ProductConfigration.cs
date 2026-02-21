using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Description).IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasData(new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 9.99m,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = 19.99m,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    Description = "Description for Product 3",
                    Price = 29.99m,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "Product 4",
                    Description = "Description for Product 4",
                    Price = 39.99m,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 5,
                    Name = "Product 5",
                    Description = "Description for Product 5",
                    Price = 49.99m,
                    CategoryId = 3
                }
            );

        }
    }
}
