using CourseProject.Application.Interfaces;
using CourseProject.Application.Services;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Presentation.Configuration.LayerConfigurations;

public static class ServiceLayerConfigurations
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
            }) 
            .AddRoles<IdentityRole>() 
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>();;

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });
        
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });
        
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
                policy.SetIsOriginAllowed(hostName => true);
            });
        });
        
        services.AddSignalR();

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IFormTemplatesService, FormTemplatesService>();
        services.AddScoped<ITopicsService, TopicsService>();
        services.AddScoped<ITagsService, TagsService>();
        services.AddScoped<ICommentsService, CommentsService>();
        services.AddScoped<IQuestionService, QuestionsService>();
        services.AddScoped<IFormsService, FormsService>();
        services.AddScoped<ILikesService, LikesService>();
        services.AddScoped<IAccessService, AccessService>();
        services.AddScoped<IImagesService, ImagesService>();
        
        services.AddScoped<SalesforceService>();
        services.AddHttpClient();

    }
}