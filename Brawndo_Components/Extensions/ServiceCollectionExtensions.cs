using Brawndo_Components.DatabaseConn;
using Brawndo_Components.Interfaces;
using Brawndo_Components.Repositories;
using Brawndo_Components.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brawndo_Components.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public const string DefaultConnectionStringName = "SchoolConnection";

        /// <summary>
        /// Registers the School data access and service layers. Every app that consumes
        /// this library calls this once, so registrations never drift between apps.
        /// </summary>
        public static IServiceCollection AddBrawndoComponents(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName = DefaultConnectionStringName)
        {
            var connectionString = configuration.GetConnectionString(connectionStringName)
                ?? throw new InvalidOperationException(
                    $"Connection string '{connectionStringName}' was not found in configuration.");

            return services.AddBrawndoComponents(connectionString);
        }

        /// <summary>
        /// Registers the School data access and service layers against an explicit
        /// connection string, for hosts that do not resolve it from IConfiguration.
        /// </summary>
        public static IServiceCollection AddBrawndoComponents(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddSingleton<ISchoolDatabaseConnection>(
                _ => new SchoolDatabaseConnection(connectionString));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IOfficeAssignmentRepository, OfficeAssignmentRepository>();
            services.AddScoped<IStudentGradeRepository, StudentGradeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseInstructorRepository, CourseInstructorRepository>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IOfficeAssignmentService, OfficeAssignmentService>();
            services.AddScoped<IStudentGradeService, StudentGradeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseInstructorService, CourseInstructorService>();

            return services;
        }
    }
}
