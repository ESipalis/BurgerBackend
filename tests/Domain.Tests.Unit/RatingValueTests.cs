using System;
using BurgerBackend.Domain.Common.Rating;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Unit;

public class RatingValueTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void CreatingRatingValue_ShouldSucceed_WhenValueIsValid(byte value)
    {
        RatingValue.From(value);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    [InlineData(150)]
    [InlineData(byte.MaxValue)]
    public void CreatingRatingValue_ShouldFail_WhenValueIsInvalid(byte value)
    {
        Func<RatingValue> createRatingValue = () => RatingValue.From(value);
        createRatingValue.Should().Throw<ArgumentException>()
            .WithMessage("Value must be between 1 and 5.");
    }
}