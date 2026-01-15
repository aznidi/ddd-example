using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.Common.Services;
using SMS.Infrastructure.Modules.Finance.Repositories;
using SMS.Infrastructure.Persistence;
using SMS.Infrastructure.Services;

namespace SMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration config )
    {
        services.AddDbContext<SmsDbContext> ( options => 
            options.UseNpgsql( config.GetConnectionString("DefaultConnection") )
        );

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IBillableServiceRepository, BillableServiceRepository>();
        services.AddScoped<IEngagementRepository, EngagementRepository>();
        
        services.AddScoped<IEngagementPdfService, EngagementPdfService>();

        return services;
    }
}
