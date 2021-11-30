namespace BurgerBackend.Infrastructure.Restaurant;

public class ReviewImageUploadException : Exception
{
    public ReviewImageUploadException(Exception exception) : base("Could not upload the image.", exception)
    {
    }
}