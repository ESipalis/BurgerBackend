using BurgerBackend.Domain.User;

namespace WebApi.Models;

public record ReviewUserDto(long UserId, string DisplayName);

public static class ReviewUserDtoExtensions
{
    public static ReviewUserDto ToDto(this User user)
    {
        return new ReviewUserDto(user.Id, user.DisplayName);
    }
}
