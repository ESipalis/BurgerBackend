using BurgerBackend.Domain.Common.Rating;

namespace BurgerBackend.Domain.Review;

public record Review(long Id, DateTimeOffset CreatedAt, Rating Rating, string? Comment, Guid? ImageId, User.User User);