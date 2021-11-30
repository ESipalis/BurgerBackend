using Microsoft.Extensions.DependencyInjection;

namespace BurgerBackend.Infrastructure.DataStore.Sql;

internal static class SqlReviewDataStoreExtensions
{
    public static IServiceCollection AddSqlReviewDataStore(this IServiceCollection serviceCollection, SqlReviewDataStoreConfiguration configuration)
    {
        serviceCollection.AddSingleton(configuration);
        return serviceCollection.AddSingleton<IReviewDataStore, SqlReviewDataStore>();
    }
}