// Models/Document.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Document
{
    [Key]
    public int DocumentID { get; set; }

    [Required(ErrorMessage = "Название документа обязательно для заполнения.")]
    [StringLength(100, ErrorMessage = "Название документа не должно превышать 100 символов.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Содержимое документа обязательно для заполнения.")]
    public string Content { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Дата создания")]
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "ID автора обязателен.")]
    public string AuthorId { get; set; }

    // Связь с пользователем (автором документа)
    public virtual User Author { get; set; }

    // Связь с тегами
    public virtual ICollection<Tag> Tags { get; set; }
}
