using BurgerBackend.Application.Restaurant;
using BurgerBackend.Domain.Common.Rating;

namespace WebApi.Models;

public record AddReviewDto(RatingDto Rating, string? Comment, byte[] Image);

public static class AddReviewDtoExtensions
{
    public static ReviewToAdd ToDomain(this AddReviewDto addReviewDto, long azureUserId, string userDisplayName)
    {
        var reviewToAddUser = new ReviewToAddUser(azureUserId, userDisplayName);
        Rating rating = addReviewDto.Rating.ToDomain();
        return new ReviewToAdd(reviewToAddUser, rating, addReviewDto.Comment, addReviewDto.Image);
    }
}
