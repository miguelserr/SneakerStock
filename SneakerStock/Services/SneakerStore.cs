using System.Collections.ObjectModel;
using SneakerStock.Models;

namespace SneakerStock.Services;

public static class SneakerStore
{
    public static ObservableCollection<Sneaker> Sneakers { get; } = new()
    {
        new Sneaker { Id = 1, Model = "Nike Air Force 1", Size = 42,   Price = 549000, Stock = 18, ImagePath = "sneaker_air_force_1.png" },
        new Sneaker { Id = 2, Model = "Air Jordan 4 Retro",   Size = 43,   Price = 899000, Stock = 6,  ImagePath = "sneaker_jordan_4.png" },
        new Sneaker { Id = 3, Model = "Nike Shox TL",         Size = 42,   Price = 749000, Stock = 4,  ImagePath = "sneaker_shox_tl.png" },
        new Sneaker { Id = 4, Model = "ASICS Gel-Kayano 14",  Size = 41,   Price = 649000, Stock = 9,  ImagePath = "sneaker_asics_kayano.png" },
        new Sneaker { Id = 5, Model = "Nike Dunk Low Panda",  Size = 42.5, Price = 599000, Stock = 14, ImagePath = "sneaker_dunk_low.png" },
        new Sneaker { Id = 6, Model = "New Balance 550",      Size = 40,   Price = 579000, Stock = 11, ImagePath = "sneaker_nb_550.png" },
        new Sneaker { Id = 7, Model = "Adidas Samba OG",      Size = 41,   Price = 529000, Stock = 20, ImagePath = "sneaker_samba.png" },
        new Sneaker { Id = 8, Model = "Air Jordan 1 High",    Size = 44,   Price = 799000, Stock = 7,  ImagePath = "sneaker_jordan_1.png" }
    };

    public static int NextId()
    {
        return Sneakers.Count == 0 ? 1 : Sneakers.Max(s => s.Id) + 1;
    }
}