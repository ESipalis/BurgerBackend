using ValueOf;

namespace BurgerBackend.Domain.Common.Location;

public class Latitude : ValueOf<double, Latitude>
{
    protected override void Validate()
    {
        if (Value is < -90 or > 90)
        {
            throw new ArgumentException("Value must be in [-90, 90] range.");
        }
    }
}