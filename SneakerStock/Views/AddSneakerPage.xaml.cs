using System.Globalization;
using SneakerStock.Models;
using SneakerStock.Services;

namespace SneakerStock.Views;

public partial class AddSneakerPage : ContentPage
{
    private string _imagePath = "sneaker.svg";

    public AddSneakerPage()
    {
        InitializeComponent();
    }

    private async void OnPickImageClicked(object? sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Elige la foto de la zapatilla",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
                return;

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(result.FileName)}";
            string destination = Path.Combine(FileSystem.AppDataDirectory, fileName);

            using Stream source = await result.OpenReadAsync();
            using FileStream target = File.Create(destination);
            await source.CopyToAsync(target);

            _imagePath = destination;
            PreviewImage.Source = ImageSource.FromFile(destination);
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        string model = ModelEntry.Text?.Trim() ?? string.Empty;
        if (model == string.Empty)
        {
            await DisplayAlertAsync("Falta el modelo", "Escribe el nombre del modelo.", "OK");
            return;
        }

        string sizeText = (SizeEntry.Text ?? string.Empty).Replace(',', '.');
        if (!double.TryParse(sizeText, NumberStyles.Float, CultureInfo.InvariantCulture, out double size) || size <= 0)
        {
            await DisplayAlertAsync("Talla no válida", "Escribe una talla mayor que 0. Ej: 42 o 42.5", "OK");
            return;
        }

        if (!int.TryParse(StockEntry.Text, out int stock) || stock < 0)
        {
            await DisplayAlertAsync("Stock no válido", "Escribe un número entero (0 o más).", "OK");
            return;
        }

        string priceDigits = new string((PriceEntry.Text ?? string.Empty).Where(char.IsDigit).ToArray());
        if (!decimal.TryParse(priceDigits, out decimal price) || price <= 0)
        {
            await DisplayAlertAsync("Precio no válido", "Escribe un precio mayor que 0.", "OK");
            return;
        }

        SneakerStore.Sneakers.Add(new Sneaker
        {
            Id = SneakerStore.NextId(),
            Model = model,
            Size = size,
            Price = price,
            Stock = stock,
            ImagePath = _imagePath
        });

        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}