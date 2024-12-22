using System;
using Microsoft.AspNet.Identity.EntityFramework;

public class ApplicationUser : IdentityUser
{
    // Добавьте дополнительные свойства, если необходимо
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    // Вы можете добавить другие свойства, которые вам нужны
}
