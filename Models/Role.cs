// Models/Role.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Role
{
    [Key]
    public int RoleID { get; set; }

    [Required(ErrorMessage = "Название роли обязательно для заполнения.")]
    [StringLength(50, ErrorMessage = "Название роли не должно превышать 50 символов.")]
    public string Name { get; set; }

    // Связь с пользователями
    public virtual ICollection<User> Users { get; set; }
}
