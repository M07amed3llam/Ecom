using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class PhotoConfigration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasData(new Photo
            {
                Id = 1,
                ImageName = "product-1.jpg",
                ProductId = 1
            },
            new Photo
            {
                Id = 2,
                ImageName = "product-2.jpg",
                ProductId = 2
            });
        }
    }
}
