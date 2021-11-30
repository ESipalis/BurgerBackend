using BurgerBackend.Domain.Review;

namespace BurgerBackend.Infrastructure.DataStore;

internal interface IReviewDataStore
{
    /// <summary>
    /// Adds review to data store.
    /// </summary>
    /// <param name="data">Review data</param>
    /// <param name="cancellationToken">Used to cancel the operation.</param>
    /// <returns>Inserted review data including the generated values.</returns>
    /// <exception cref="BurgerBackend.Domain.Restaurant.NoRestaurantFoundException">thrown when the restaurant cannot be found.</exception>
    Task<ReviewData> AddReviewAsync(ReviewDataToAdd data, CancellationToken cancellationToken = default);
}