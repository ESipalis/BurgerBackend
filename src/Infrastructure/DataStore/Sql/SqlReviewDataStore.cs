using BurgerBackend.Application.Restaurant;
using BurgerBackend.Domain.Common.Rating;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BurgerBackend.Infrastructure.DataStore.Sql;

internal class SqlReviewDataStore : IReviewDataStore
{
    private readonly SqlReviewDataStoreConfiguration _configuration;

    public SqlReviewDataStore(SqlReviewDataStoreConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ReviewData> AddReviewAsync(ReviewDataToAdd data, CancellationToken cancellationToken = default)
    {
        await using var sqlConnection = new SqlConnection(_configuration.SqlConnectionString);
        await sqlConnection.OpenAsync(cancellationToken);
        long userId = await InsertUserIfDoesNotExistAsync(sqlConnection, data.User, cancellationToken);
        return await InsertReviewDataAsync(sqlConnection, userId, data, cancellationToken);
    }


    private static async Task<ReviewData> InsertReviewDataAsync(SqlConnection sqlConnection, long userId, ReviewDataToAdd data, CancellationToken cancellationToken)
    {
        const string insertSql = @"-- noinspection SqlResolve
        INSERT INTO dbo.Reviews
        (RestaurantId, UserId, TasteRating, TextureRating, VisualPresentationRating, Comment, ImageId)
        OUTPUT inserted.Id, inserted.RestaurantId, inserted.UserId, inserted.CreatedAt, inserted.TasteRating, inserted.TextureRating, inserted.VisualPresentationRating, inserted.Comment, inserted.ImageId
        VALUES (@RestaurantId, @UserId, @TasteRating, @TextureRating, @VisualPresentationRating, @Comment, @ImageId)";
        Rating rating = data.Rating;
        var parameters = new
        {
            RestaurantId = data.RestaurantId,
            UserId = userId,
            TasteRating = rating.Taste,
            TextureRating = rating.Texture,
            VisualPresentationRating = rating.VisualPresentation,
            Comment = data.Comment,
            ImageId = data.ImageId
        };
        var command = new CommandDefinition(insertSql, parameters, cancellationToken: cancellationToken);
        return await sqlConnection.QuerySingleAsync<ReviewData>(command);
    }
    
    private static Task<long> InsertUserIfDoesNotExistAsync(SqlConnection sqlConnection, ReviewToAddUser dataUser, CancellationToken cancellationToken)
    {
        // TODO insert and return user id.
        throw new NotImplementedException();
    }
}