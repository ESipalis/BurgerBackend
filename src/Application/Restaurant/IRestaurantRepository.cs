using BurgerBackend.Domain.Review;

namespace BurgerBackend.Application.Restaurant;

public interface IRestaurantRepository
{
    /// <summary>
    /// Adds review to restaurant's reviews.
    /// </summary>
    /// <param name="restaurantId">Id of the restaurant to add the review to.</param>
    /// <param name="reviewToAdd">Review data</param>
    /// <param name="cancellationToken">Used to cancel the operation.</param>
    /// <returns>Inserted review data</returns>
    /// <exception cref="BurgerBackend.Domain.Restaurant.NoRestaurantFoundException">thrown when the restaurant cannot be found.</exception>
    Task<Review> AddReviewAsync(long restaurantId, ReviewToAdd reviewToAdd, CancellationToken cancellationToken = default);
}