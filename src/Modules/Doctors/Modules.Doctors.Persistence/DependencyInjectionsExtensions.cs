using Common.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Doctors.Domain.Abstractions;
using Modules.Doctors.Persistence.Constants;
using Modules.Doctors.Persistence.Repositories;

namespace Modules.Doctors.Persistence;

public static class DependencyInjectionsExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDbContext<DoctorsDbContext>(options
                => options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    x => x.MigrationsHistoryTable(
                        PersistenceConstants.MigrationsHistoryTable,
                        PersistenceConstants.DefaultSchema)))
            .AddScoped<IDoctorsUnitOfWork, DoctorsUnitOfWork>()
            .AddScoped<IDoctorRepository, DoctorRepository>()
            .AddScoped<IDoctorRegistrationRepository, DoctorRegistrationRepository>()
            .AddScoped<IDoctorShiftRepository, DoctorShiftRepository>();

    public static void UsePersistence(this IApplicationBuilder app)
        => app.ApplyMigrations<DoctorsDbContext>();
}
