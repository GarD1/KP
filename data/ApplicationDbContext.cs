// Data/ApplicationDbContext.cs
using System.Data.Entity;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext() : base("DefaultConnection") // Использует строку подключения из Web.config
    {
    }
    public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();
    }
    // DbSet для каждой модели
    public DbSet<User> Users { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка отношений между моделями
        modelBuilder.Entity<Document>()
            .HasRequired(d => d.Author)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.AuthorId);

        modelBuilder.Entity<DocumentTag>()
            .HasKey(dt => new { dt.DocumentID, dt.TagID });

        modelBuilder.Entity<DocumentTag>()
            .HasRequired(dt => dt.Document)
            .WithMany(d => (System.Collections.Generic.ICollection<DocumentTag>)d.Tags)
            .HasForeignKey(dt => dt.DocumentID);

        modelBuilder.Entity<DocumentTag>()
            .HasRequired(dt => dt.Tag)
            .WithMany(t => (System.Collections.Generic.ICollection<DocumentTag>)t.Documents)
            .HasForeignKey(dt => dt.TagID);
    }
}

// Для связи многие-ко-многим между Document и Tag
public class DocumentTag
{
    public int DocumentID { get; set; }
    public Document Document { get; set; }

    public int TagID { get; set; }
    public Tag Tag { get; set; }
}
