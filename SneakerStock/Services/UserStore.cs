using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using SneakerStock.Models;

namespace SneakerStock.Services;

public static class UserStore
{
    private const string StorageKey = "users";

    private static readonly List<User> _users = Load();

    public static User? CurrentUser { get; set; }

    private static List<User> Load()
    {
        string json = Preferences.Default.Get(StorageKey, string.Empty);

        List<User> list = string.IsNullOrEmpty(json)
            ? new List<User>()
            : JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

        foreach (User old in list.Where(u => string.IsNullOrWhiteSpace(u.Username)))
        {
            old.Username = old.Email.Split('@')[0].ToLowerInvariant();
        }

        if (!list.Any(u => u.Username == "admin"))
        {
            list.Add(new User
            {
                Username = "admin",
                FirstName = "Administrador",
                LastName = "Sneaker",
                Email = "admin@sneaker.com",
                Phone = "3000000000",
                StoreName = "Sneaker Stock",
                Role = "Administrador",
                PasswordHash = Hash("1234")
            });
        }

        return list;
    }

    private static void Save()
    {
        string json = JsonSerializer.Serialize(_users);
        Preferences.Default.Set(StorageKey, json);
    }

    private static string Hash(string text)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        return Convert.ToHexString(bytes);
    }

    public static string? Register(User user, string password)
    {
        string username = user.Username.Trim().ToLowerInvariant();
        string email = user.Email.Trim().ToLowerInvariant();

        if (_users.Any(u => u.Username == username))
            return "Ese nombre de usuario ya está en uso.";

        if (_users.Any(u => u.Email == email))
            return "Ya existe una cuenta con ese correo.";

        user.Username = username;
        user.Email = email;
        user.PasswordHash = Hash(password);

        _users.Add(user);
        Save();
        return null;
    }

    public static User? Validate(string username, string password)
    {
        string normalized = username.Trim().ToLowerInvariant();
        string hash = Hash(password);

        return _users.FirstOrDefault(u => u.Username == normalized && u.PasswordHash == hash);
    }
}