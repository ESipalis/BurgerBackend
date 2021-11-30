using BurgerBackend.Domain.Common.Rating;

namespace BurgerBackend.Application.Restaurant;

public record ReviewToAdd(ReviewToAddUser User, Rating Rating, string? Comment, byte[]? ImageBytes);

public record ReviewToAddUser(long AzureUserId, string DisplayName);