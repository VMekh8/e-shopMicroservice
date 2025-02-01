using Catalog.API.Models;
using Marten;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitializeData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
        {
            return;
        }

        session.Store<Product>(GetConfiguredData());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetConfiguredData()
    {
        List<Product> products =
        [
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple iPhone 15",
                Category = ["Electronics", "Smartphones"],
                Description = "Latest iPhone model with advanced camera and A16 Bionic chip.",
                ImageFile = "iphone15.jpg",
                Price = 999.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S23",
                Category = ["Electronics", "Smartphones"],
                Description = "Flagship Samsung smartphone with Dynamic AMOLED display.",
                ImageFile = "galaxy_s23.jpg",
                Price = 899.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sony WH-1000XM5",
                Category = ["Electronics", "Headphones"],
                Description = "Premium noise-canceling wireless headphones.",
                ImageFile = "sony_wh1000xm5.jpg",
                Price = 399.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Dell XPS 15",
                Category = ["Electronics", "Laptops"],
                Description = "High-performance laptop with Intel Core i9 and OLED display.",
                ImageFile = "dell_xps15.jpg",
                Price = 1899.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Logitech MX Master 3S",
                Category = ["Electronics", "Accessories"],
                Description = "Ergonomic wireless mouse with precision tracking.",
                ImageFile = "mx_master_3s.jpg",
                Price = 99.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Nike Air Max 270",
                Category = ["Fashion", "Shoes"],
                Description = "Stylish and comfortable sneakers with Air cushioning.",
                ImageFile = "nike_airmax_270.jpg",
                Price = 129.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "The Pragmatic Programmer",
                Category = ["Books", "Programming"],
                Description = "Classic book on software development and engineering best practices.",
                ImageFile = "pragmatic_programmer.jpg",
                Price = 49.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "LEGO Technic Porsche 911 RSR",
                Category = ["Toys", "LEGO"],
                Description = "Detailed LEGO model of the Porsche 911 RSR race car.",
                ImageFile = "lego_porsche_911_rsr.jpg",
                Price = 149.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung 4K Smart TV 55\"",
                Category = ["Electronics", "TVs"],
                Description = "Ultra HD 4K Smart TV with Quantum Dot technology.",
                ImageFile = "samsung_4k_tv.jpg",
                Price = 699.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bose SoundLink Revolve+",
                Category = ["Electronics", "Speakers"],
                Description = "Portable Bluetooth speaker with 360-degree sound.",
                ImageFile = "bose_revolve.jpg",
                Price = 299.99m
            }
        ];

        return products;
    }
}