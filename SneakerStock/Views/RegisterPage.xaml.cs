using System.Net.Mail;
using SneakerStock.Models;
using SneakerStock.Services;

namespace SneakerStock.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private void OnTogglePasswordClicked(object? sender, EventArgs e)
    {
        bool hide = !PasswordEntry.IsPassword;

        PasswordEntry.IsPassword = hide;
        ConfirmEntry.IsPassword = hide;
        EyeButton.Source = hide ? "ic_eye.png" : "ic_eye_off.png";
    }

    private async void OnRegisterClicked(object? sender, EventArgs e)
    {
        string firstName = FirstNameEntry.Text?.Trim() ?? string.Empty;
        string lastName = LastNameEntry.Text?.Trim() ?? string.Empty;
        string email = EmailEntry.Text?.Trim() ?? string.Empty;
        string phone = PhoneEntry.Text?.Trim() ?? string.Empty;
        string store = StoreEntry.Text?.Trim() ?? string.Empty;
        string role = RolePicker.SelectedItem?.ToString() ?? string.Empty;
        string username = UsernameEntry.Text?.Trim() ?? string.Empty;
        string password = PasswordEntry.Text ?? string.Empty;
        string confirm = ConfirmEntry.Text ?? string.Empty;

        // Nos quedamos solo con los dígitos del teléfono
        string phoneDigits = new string(phone.Where(char.IsDigit).ToArray());

        if (firstName == string.Empty)
        {
            await DisplayAlertAsync("Falta el nombre", "Escribe tus nombres.", "OK");
            return;
        }

        if (lastName == string.Empty)
        {
            await DisplayAlertAsync("Faltan los apellidos", "Escribe tus apellidos.", "OK");
            return;
        }

        if (!MailAddress.TryCreate(email, out _))
        {
            await DisplayAlertAsync("Correo no válido", "Escribe un correo como nombre@correo.com", "OK");
            return;
        }

        if (phoneDigits.Length < 7 || phoneDigits.Length > 15)
        {
            await DisplayAlertAsync("Teléfono no válido", "Escribe un número de entre 7 y 15 dígitos.", "OK");
            return;
        }

        if (store == string.Empty)
        {
            await DisplayAlertAsync("Falta la tienda", "Escribe el nombre de tu tienda o negocio.", "OK");
            return;
        }

        if (role == string.Empty)
        {
            await DisplayAlertAsync("Falta el cargo", "Selecciona tu cargo.", "OK");
            return;
        }

        // Usuario: 4 a 20 caracteres, solo letras, números, punto o guion bajo
        bool validUsername = username.Length >= 4
                             && username.Length <= 20
                             && username.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '_');

        if (!validUsername)
        {
            await DisplayAlertAsync("Usuario no válido",
                "Usa entre 4 y 20 caracteres: letras, números, punto o guion bajo (sin espacios).", "OK");
            return;
        }

        if (password.Length < 6)
        {
            await DisplayAlertAsync("Contraseña muy corta", "Debe tener mínimo 6 caracteres.", "OK");
            return;
        }

        if (password != confirm)
        {
            await DisplayAlertAsync("No coinciden", "Las contraseñas no son iguales.", "OK");
            return;
        }

        if (!TermsCheck.IsChecked)
        {
            await DisplayAlertAsync("Términos y condiciones", "Debes aceptar los términos para continuar.", "OK");
            return;
        }

        // Todo correcto: armamos el usuario y lo registramos
        User newUser = new User
        {
            Username = username,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phoneDigits,
            StoreName = store,
            Role = role
        };

        string? error = UserStore.Register(newUser, password);

        if (error != null)
        {
            await DisplayAlertAsync("No se pudo crear la cuenta", error, "OK");
            return;
        }

        await DisplayAlertAsync("¡Cuenta creada!",
            $"Bienvenido, {firstName}. Entra con el usuario \"{newUser.Username}\".", "OK");
        await Shell.Current.GoToAsync("..");
    }

    // Botón "←"
    private async void OnBackClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    // Enlace "Inicia sesión"
    private async void OnBackTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}