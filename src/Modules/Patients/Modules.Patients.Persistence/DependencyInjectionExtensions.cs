using Common.Shared.Extensions;
using Common.Shared.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Patients.Domain.Abstractions;
using Modules.Patients.Domain.Entities;
using Modules.Patients.Persistence.Constants;
using Modules.Patients.Persistence.Repositories;

namespace Modules.Patients.Persistence;

public static class DependencyInjectionsExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDbContext<PatientsDbContext>(options
                => options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    x => x.MigrationsHistoryTable(
                        PersistenceConstants.MigrationsHistoryTable,
                        PersistenceConstants.DefaultSchema)))
            .AddScoped<IPatientsUnitOfWork, PatientsUnitOfWork>()
            .AddScoped<IPatientRepository, PatientRepository>()
            .AddScoped<
                IRepository<PatientAppointment>,
                Repository<PatientsDbContext, PatientAppointment>>();

    public static void UsePersistence(this IApplicationBuilder app)
        => app.ApplyMigrations<PatientsDbContext>();
}
