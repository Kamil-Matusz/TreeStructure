using Microsoft.EntityFrameworkCore;

namespace TreeStructure.DAL
{
    public static class Extenions
    {
        private const string SectionName = "database";

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(SectionName);
            services.Configure<DatabaseOptions>(section);
            var options = configuration.GetOptions<DatabaseOptions>(SectionName);


            services.AddDbContext<TreeDbContext>(x => x.UseSqlServer(options.connectionString));
            services.AddHostedService<DatabaseInitializer>();

            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
