using BurgerBackend.Domain.Common.Rating;
using BurgerBackend.Domain.Review;
using BurgerBackend.Domain.User;

namespace BurgerBackend.Infrastructure.DataStore;

internal record ReviewData
{
    public long Id { get; set; }
    public long RestaurantId { get; set; }
    public long UserId { get; set; }
    public string UserDisplayName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public byte TasteRating { get; set; }
    public byte TextureRating { get; set; }
    public byte VisualPresentationRating { get; set; }
    public string? Comment { get; set; }
    public Guid? ImageId { get; set; }
}

internal static class ReviewDataExtensions
{
    public static Review ToDomain(this ReviewData reviewData)
    {
        var rating = new Rating(RatingValue.From(reviewData.TasteRating), RatingValue.From(reviewData.TextureRating), RatingValue.From(reviewData.VisualPresentationRating));
        var user = new User(reviewData.UserId, reviewData.UserDisplayName);
        return new Review(reviewData.Id, reviewData.CreatedAt, rating, reviewData.Comment, reviewData.ImageId, user);
    }
}