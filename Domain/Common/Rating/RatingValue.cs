using ValueOf;

namespace BurgerBackend.Domain.Common.Rating;

public class RatingValue : ValueOf<byte, RatingValue>
{
    protected override void Validate()
    {
        if (Value is < 1 or > 5)
        {
            throw new ArgumentException("Value must be between 1 and 5.");
        }
    }
}