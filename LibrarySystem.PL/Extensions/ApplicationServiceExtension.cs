using AutoMapper;
using LibrarySystem.BLL.Implementations;
using LibrarySystem.BLL.Services;
using System.Reflection;

namespace LibrarySystem.PL.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();

            services.AddSingleton<IMapper>(_ =>
            {
                var config = new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
                return config.CreateMapper();
            });

            return services;
        }
    }
}