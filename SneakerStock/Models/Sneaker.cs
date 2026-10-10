namespace SneakerStock.Models;

public class Sneaker : BindingUtilObject
{
    private int _stock;

    public int Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public double Size { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; } = "sneaker.svg";

    public int Stock
    {
        get => _stock;
        set => SetProperty(ref _stock, value);
    }
}