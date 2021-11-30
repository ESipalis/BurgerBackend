using BurgerBackend.Application.Restaurant;
using BurgerBackend.Domain.Review;
using BurgerBackend.Infrastructure.DataStore;
using BurgerBackend.Infrastructure.ImageStore;
using Microsoft.Extensions.Logging;

namespace BurgerBackend.Infrastructure.Restaurant;

internal class DbAndImageStoreRestaurantRepository : IRestaurantRepository
{
    private readonly IReviewDataStore _reviewDataStore;
    private readonly IImageStore _imageStore;
    private readonly ILogger<DbAndImageStoreRestaurantRepository> _logger;

    public DbAndImageStoreRestaurantRepository(IReviewDataStore reviewDataStore, IImageStore imageStore, ILogger<DbAndImageStoreRestaurantRepository> logger)
    {
        _reviewDataStore = reviewDataStore;
        _imageStore = imageStore;
        _logger = logger;
    }

    public async Task<Review> AddReviewAsync(long restaurantId, ReviewToAdd reviewToAdd, CancellationToken cancellationToken = default)
    {
        Guid? imageGuid = await UploadImageIfNotNullAsync(reviewToAdd.ImageBytes);

        try
        {
            var reviewDataToAdd = new ReviewDataToAdd(restaurantId, reviewToAdd.User, reviewToAdd.Rating, reviewToAdd.Comment, imageGuid);
            ReviewData reviewData = await _reviewDataStore.AddReviewAsync(reviewDataToAdd, cancellationToken);
            return reviewData.ToDomain();
        }
        catch (Exception e)
        {
            await DeleteImageIfUploadedAsync(imageGuid);
            throw new ReviewDataSaveException(e);
        }
    }

    private async Task<Guid?> UploadImageIfNotNullAsync(byte[]? imageBytes)
    {
        if (imageBytes == null)
        {
            return null;
        }

        try
        {
            Guid imageGuid = await _imageStore.StoreImageAsync(imageBytes);
            _logger.LogTrace("Image saved successfully with guid: {ImageGuid}", imageGuid);
            return imageGuid;
        }
        catch (Exception e)
        {
            throw new ReviewImageUploadException(e);
        }
    }

    private async Task DeleteImageIfUploadedAsync(Guid? imageGuid)
    {
        if (imageGuid == null)
        {
            return;
        }

        _logger.LogTrace("An error occurred while trying to save the review, deleting uploaded image.");
        try
        {
            await _imageStore.DeleteImageAsync(imageGuid.Value);
        }
        catch (Exception)
        {
            // TODO: Either send an event or create "orphaned image cleanup service" and just ignore the exception here.
        }
    }
}