using BurgerBackend.Domain.Common.Rating;

namespace BurgerBackend.Domain.Restaurant;

public record RestaurantRating(Rating? AverageRating, int NumberOfReviews);