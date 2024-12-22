// Models/User.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string UserId { get; set; } // Обычно строка, например, Guid

    [Required(ErrorMessage = "Имя пользователя обязательно для заполнения.")]
    [StringLength(50, ErrorMessage = "Имя пользователя не должно превышать 50 символов.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email обязателен.")]
    [EmailAddress(ErrorMessage = "Неверный формат email.")]
    public string Email { get; set; }

    // Связь с документами
    public virtual ICollection<Document> Documents { get; set; }
}
