using BurgerBackend.Domain.Common.Rating;

namespace BurgerBackend.Application.Restaurant;

public record ReviewToAdd(Rating Rating, string? Comment, byte[]? ImageBytes);