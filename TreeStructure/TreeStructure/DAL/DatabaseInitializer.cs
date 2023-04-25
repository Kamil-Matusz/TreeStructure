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

                var tree = dbContext.Trees.ToList();
                if(!tree.Any()) 
                {
                    tree = new List<Tree>
                    {
                        new Tree("Root",null),
                        new Tree("Dokumenty",1),
                        new Tree("Filmy",2),
                    };
                    dbContext.AddRange(tree);
                    dbContext.SaveChanges();
                }
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
