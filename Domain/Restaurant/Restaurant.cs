using BurgerBackend.Domain.Common;
using BurgerBackend.Domain.Common.Location;

namespace BurgerBackend.Domain.Restaurant;

public record Restaurant(long Id, string Name, string Description, Location Location, OpenHours OpenHours, RestaurantRating Rating);

