using BurgerBackend.Domain.Common.Rating;

namespace WebApi.Models;

public record RatingDto(byte Taste, byte Texture, byte VisualPresentation);

public static class RatingDtoExtensions
{
    public static RatingDto ToDto(this Rating rating)
    {
        return new RatingDto(rating.Taste.Value, rating.Texture.Value, rating.VisualPresentation.Value);
    }

    public static Rating ToDomain(this RatingDto ratingDto)
    {
        (byte taste, byte texture, byte visualPresentation) = ratingDto;
        RatingValue tasteRatingValue = RatingValue.From(taste);
        RatingValue textureRatingValue = RatingValue.From(texture);
        RatingValue visualPresentationRatingValue = RatingValue.From(visualPresentation);
        return new Rating(tasteRatingValue, textureRatingValue, visualPresentationRatingValue);
    }
}