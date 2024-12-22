// Models/Tag.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Tag
{
    [Key]
    public int TagID { get; set; }

    [Required(ErrorMessage = "Название тега обязательно для заполнения.")]
    [StringLength(30, ErrorMessage = "Название тега не должно превышать 30 символов.")]
    public string Name { get; set; }

    // Связь с документами
    public virtual ICollection<Document> Documents { get; set; }
}
