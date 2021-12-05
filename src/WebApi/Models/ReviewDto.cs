using BurgerBackend.Domain.Review;

namespace WebApi.Models;

public record ReviewDto(long Id, long RestaurantId, ReviewUserDto User, DateTimeOffset CreatedAt, RatingDto Rating, string? Comment, Guid? ImageId);

public static class ReviewDtoExtensions
{
    public static ReviewDto ToDto(this Review review, long restaurantId)
    {
        return new ReviewDto(review.Id, restaurantId, review.User.ToDto(), review.CreatedAt, review.Rating.ToDto(), review.Comment, review.ImageId);
    }
}
