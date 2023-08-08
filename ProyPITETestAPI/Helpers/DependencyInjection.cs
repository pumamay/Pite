using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Proyecto.PiteApi.Data;
using Proyecto.PiteApi.Data.Connections;
using Proyecto.PiteApi.Helpers;
using Proyecto.PiteApi.Interfaces;
using Proyecto.PiteApi.Interfaces.Contracts;
using Proyecto.PiteApi.Services;
using System.Text;

namespace AdminConta.AuthAPI.Helpers;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PiteDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPiteDbContext>(provider => provider.GetRequiredService<PiteDbContext>());
        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddCors();

        services.AddEndpointsApiExplorer();
        services.AddControllers().AddNewtonsoftJson();

        services.AddScoped<IApplicationWriteDbConnection, ApplicationWriteDbConnection>();
        services.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();

        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddTransient(typeof(IReadRepository<>), typeof(BaseRepository<>));
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
