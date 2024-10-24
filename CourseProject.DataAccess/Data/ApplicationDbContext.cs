using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Data;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<FormAnswer> FormAnswers { get; set; }
    public DbSet<FormTemplate> FormTemplates { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Topic> Topics { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Связь один-ко-многим: FormTemplate и Comments
        builder.Entity<Comment>()
            .HasOne(c => c.FormTemplate)
            .WithMany(ft => ft.Comments)
            .HasForeignKey(c => c.FormTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь один-ко-многим: ApplicationUser и Comments
        builder.Entity<Comment>()
            .HasOne(c => c.ApplicationUser)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь один-ко-многим: FormTemplate и Forms
        builder.Entity<Form>()
            .HasOne(f => f.FormTemplate)
            .WithMany(ft => ft.Forms)
            .HasForeignKey(f => f.FormTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь один-ко-многим: ApplicationUser и Forms
        builder.Entity<Form>()
            .HasOne(f => f.ApplicationUser)
            .WithMany(u => u.Forms)
            .HasForeignKey(f => f.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Связь один-ко-многим: Question и FormAnswer
        builder.Entity<Question>()
            .HasMany(q => q.FormAnswers)
            .WithOne(fa => fa.Question)
            .HasForeignKey(fa => fa.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь один-ко-многим: ApplicationUser и FormTemplate (Creator)
        builder.Entity<FormTemplate>()
            .HasOne(ft => ft.Creator)
            .WithMany(u => u.FormTemplates)
            .HasForeignKey(ft => ft.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Связь многие-ко-многим: FormTemplate и Tags
        builder.Entity<FormTemplate>()
            .HasMany(ft => ft.Tags)
            .WithMany(t => t.FormTemplates);

        // Связь один-ко-многим: Topic и FormTemplates
        builder.Entity<FormTemplate>()
            .HasOne(ft => ft.Topic)
            .WithMany(t => t.FormTemplates)
            .HasForeignKey(ft => ft.TopicId)
            .OnDelete(DeleteBehavior.Restrict);

        // Связь один-ко-многим: Like и FormTemplate
        builder.Entity<Like>()
            .HasOne(l => l.FormTemplate)
            .WithMany(ft => ft.Likes)
            .HasForeignKey(l => l.FormTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь один-ко-многим: ApplicationUser и Like
        builder.Entity<Like>()
            .HasOne(l => l.ApplicationUser)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Topic>().HasData(
            new Topic { Id = Guid.NewGuid(), Name = "Education" },
            new Topic { Id = Guid.NewGuid(), Name = "Test" },
            new Topic { Id = Guid.NewGuid(), Name = "Other" }
        );
        
        builder.Entity<Tag>().HasData(
            new Topic { Id = Guid.NewGuid(), Name = "Tag 1" },
            new Topic { Id = Guid.NewGuid(), Name = "Tag 2" },
            new Topic { Id = Guid.NewGuid(), Name = "Tag 3" }
        );
        
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}