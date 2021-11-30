using BurgerBackend.Application.Restaurant;
using BurgerBackend.Domain.Common.Rating;
using BurgerBackend.Domain.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class RestaurantsController : ControllerBase
{

    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantsController(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    [HttpPost("{restaurantId:long}/reviews")]
    public async Task<ActionResult<ReviewDto>> AddReview(long restaurantId, AddReviewDto addReviewDto, CancellationToken cancellationToken)
    {
        var azureUserId = Convert.ToInt64(this.User.Claims.First(claim => claim.Type == "AzureUserId").Value);
        string displayName = this.User.Claims.First(claim => claim.Type == "DisplayName").Value;
        var reviewToAdd = addReviewDto.ToReviewToAdd(azureUserId, displayName);
        Review review = await _restaurantRepository.AddReviewAsync(restaurantId, reviewToAdd, cancellationToken);
        return review.ToDto();
    }
}

public record AddReviewDto();

public static class AddReviewDtoExtensions
{
    public static ReviewToAdd ToReviewToAdd(this AddReviewDto addReviewDto, long azureUserId, string userDisplayName)
    {
        var rating = new Rating(RatingValue.From(1), RatingValue.From(2), RatingValue.From(3));
        return new ReviewToAdd(new ReviewToAddUser(azureUserId, userDisplayName), rating, "FakeComment", null);
    }
}

public record ReviewDto();

public static class ReviewDtoExtensions
{
    public static ReviewDto ToDto(this Review review)
    {
        return new ReviewDto();
    }
}