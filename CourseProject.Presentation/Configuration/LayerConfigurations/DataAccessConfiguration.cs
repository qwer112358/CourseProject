using CourseProject.DataAccess.Data;
using CourseProject.DataAccess.Repository;
using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Presentation.Configuration.LayerConfigurations;

public static class DataAccessConfiguration
{
    public static void ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<IFormTemplatesRepository, FormTemplatesRepository>();
        services.AddScoped<ITopicsRepository, TopicsRepository>();
        services.AddScoped<ITagsRepository, TagsRepository>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();
    }
}