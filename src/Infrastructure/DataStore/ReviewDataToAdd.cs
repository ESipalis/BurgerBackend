using BurgerBackend.Application.Restaurant;
using BurgerBackend.Domain.Common.Rating;

namespace BurgerBackend.Infrastructure.DataStore;

internal record ReviewDataToAdd(long RestaurantId, ReviewToAddUser User, Rating Rating, string? Comment, Guid? ImageId);