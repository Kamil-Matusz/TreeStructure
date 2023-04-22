using Microsoft.EntityFrameworkCore;
using TreeStructure.Entities;

namespace TreeStructure.DAL
{
    internal sealed class DatabaseInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TreeDbContext>();
                dbContext.Database.Migrate();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
