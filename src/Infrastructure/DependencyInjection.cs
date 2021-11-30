using BurgerBackend.Infrastructure.DataStore.Sql;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, SqlReviewDataStoreConfiguration sqlReviewDataStoreConfiguration)
    {
        services.AddSqlReviewDataStore(sqlReviewDataStoreConfiguration);
        return services;
    }
}