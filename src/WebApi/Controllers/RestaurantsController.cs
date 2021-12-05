using BurgerBackend.Application.Restaurant;
using BurgerBackend.Domain.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using WebApi.Models;

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
        ReviewToAdd reviewToAdd = GetReviewToAdd(addReviewDto);
        Review review = await _restaurantRepository.AddReviewAsync(restaurantId, reviewToAdd, cancellationToken);
        return review.ToDto(restaurantId);
    }

    private ReviewToAdd GetReviewToAdd(AddReviewDto addReviewDto)
    {
        const string displayNameClaim = "DisplayName";
        const string azureUserIdClaim = "AzureUserId";
        long azureUserId = long.Parse(User.Claims.First(claim => claim.Type == azureUserIdClaim).Value);
        string displayName = User.Claims.First(claim => claim.Type == displayNameClaim).Value;
        return addReviewDto.ToDomain(azureUserId, displayName);
    }
}