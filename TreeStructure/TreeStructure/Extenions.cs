using TreeStructure.DAL;

namespace TreeStructure
{
    public static class Extensions
    {
        private const string SectionName = "database";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
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
