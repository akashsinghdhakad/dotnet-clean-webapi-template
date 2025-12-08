namespace dotnetWebApiCoreCBA.Models.Entities;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    // Base64-encoded PBKDF2 hash
    public string PasswordHash { get; set; } = string.Empty;

    // Base64-encoded salt
    public string PasswordSalt { get; set; } = string.Empty;

    public string Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
