using Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Data
{
    public static class DataExtension
    {
        /// <summary>
        ///     Adds the DBContext to the given serviceCollection
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration">
        ///     The Configuration containing the Config for the Db-Connection
        ///     Database:ConnectionString = Connectionstring
        ///     Database:Name = The name of the Database
        /// </param>
        public static void AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddTransient(s =>
                new MongoClient(configuration["Database:ConnectionString"])
                    .GetDatabase(configuration["Database:Name"]));
            serviceCollection.AddSingleton<DbContext>();
        }

        /// <summary>
        ///     Adds the BugStore to the given serviceCollection
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddBugStore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(provider => provider.GetService<DbContext>().Bugs);
            
            serviceCollection.AddTransient<Bug.IBugRepository, BugRepository>();
         
        }
    }
}