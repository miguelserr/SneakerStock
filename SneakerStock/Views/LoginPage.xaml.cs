using SneakerStock.Models;
using SneakerStock.Services;

namespace SneakerStock.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnTogglePasswordClicked(object? sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        EyeButton.Source = PasswordEntry.IsPassword ? "ic_eye.png" : "ic_eye_off.png";
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        string username = UserEntry.Text?.Trim() ?? string.Empty;
        string password = PasswordEntry.Text ?? string.Empty;

        if (username == string.Empty || password == string.Empty)
        {
            await DisplayAlertAsync("Datos incompletos", "Escribe tu usuario y contraseña.", "OK");
            return;
        }

        User? user = UserStore.Validate(username, password);

        if (user == null)
        {
            await DisplayAlertAsync("Error", "Usuario o contraseña incorrectos.", "OK");
            return;
        }

        UserStore.CurrentUser = user;
        PasswordEntry.Text = string.Empty;

        await Shell.Current.GoToAsync("//SneakersPage");
    }

    private async void OnRegisterTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }
}