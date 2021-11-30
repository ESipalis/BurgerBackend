namespace BurgerBackend.Domain.Restaurant;

public class NoRestaurantFoundException : Exception
{
    public long RestaurantId { get; }

    public NoRestaurantFoundException(long restaurantId) : base($"No restaurant found with the provided id: {restaurantId}")
    {
        RestaurantId = restaurantId;
    }
}