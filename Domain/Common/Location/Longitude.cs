using ValueOf;

namespace BurgerBackend.Domain.Common.Location;

public class Longitude : ValueOf<double, Longitude>
{
    protected override void Validate()
    {
        if (Value is < -180 or > 180)
        {
            throw new ArgumentException("Value must be in [-180, 180] range.");
        }
    }
}