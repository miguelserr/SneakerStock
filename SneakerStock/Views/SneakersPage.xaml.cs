using System.Collections.ObjectModel;
using SneakerStock.Models;
using SneakerStock.Services;

namespace SneakerStock.Views;

public partial class SneakersPage : ContentPage
{
    public ObservableCollection<Sneaker> Sneakers => SneakerStore.Sneakers;
    public int TotalModels => Sneakers.Count;
    public int TotalPairs => Sneakers.Sum(s => s.Stock);

    public SneakersPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshTotals();
    }

    private void RefreshTotals()
    {
        OnPropertyChanged(nameof(TotalModels));
        OnPropertyChanged(nameof(TotalPairs));
    }

    private void OnIncreaseClicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Sneaker sneaker)
        {
            sneaker.Stock++;
            RefreshTotals();
        }
    }

    private void OnDecreaseClicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Sneaker sneaker)
        {
            if (sneaker.Stock > 0)
            {
                sneaker.Stock--;
                RefreshTotals();
            }
        }
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AddSneakerPage");
    }
}