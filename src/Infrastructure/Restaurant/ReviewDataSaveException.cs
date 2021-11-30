namespace BurgerBackend.Infrastructure.Restaurant;

public class ReviewDataSaveException : Exception
{
    public ReviewDataSaveException(Exception innerException) : base("Could not save review data.", innerException)
    {
    }
}