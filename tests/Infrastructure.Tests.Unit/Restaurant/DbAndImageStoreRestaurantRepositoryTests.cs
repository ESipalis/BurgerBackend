using System;
using System.Threading.Tasks;
using BurgerBackend.Application.Restaurant;
using BurgerBackend.Domain.Common.Rating;
using BurgerBackend.Infrastructure.DataStore;
using BurgerBackend.Infrastructure.ImageStore;
using BurgerBackend.Infrastructure.Restaurant;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Unit.Restaurant;

public class DbAndImageStoreRestaurantRepositoryTests
{

    private readonly DbAndImageStoreRestaurantRepository _sut;
    private readonly Mock<IImageStore> _imageStoreMock = new();
    private readonly Mock<IReviewDataStore> _reviewDataStoreMock = new();


    public DbAndImageStoreRestaurantRepositoryTests()
    {
        _imageStoreMock = new Mock<IImageStore>();
        _reviewDataStoreMock = new Mock<IReviewDataStore>();
        _sut = new DbAndImageStoreRestaurantRepository(_reviewDataStoreMock.Object, _imageStoreMock.Object, new NullLogger<DbAndImageStoreRestaurantRepository>());
    }

    [Fact]
    public async Task AddReview_ShouldSaveImageAndSaveToDb_WhenImageExists()
    {
        // Arrange
        var reviewToAddUser = new ReviewToAddUser(1, "TestName");
        var rating = new Rating(RatingValue.From(1), RatingValue.From(2), RatingValue.From(5));
        var imageBytes = new byte[]{1, 2, 3};
        var reviewToAdd = new ReviewToAdd(reviewToAddUser, rating, "TestComment", imageBytes);
        var imageGuid = Guid.NewGuid();
        _imageStoreMock.Setup(x => x.StoreImageAsync(It.IsAny<byte[]>(), default))
            .ReturnsAsync(imageGuid);
        var reviewData = new ReviewData
        {
            TasteRating = 1,
            TextureRating = 2,
            VisualPresentationRating = 5
        };
        _reviewDataStoreMock.Setup(x => x.AddReviewAsync(It.IsAny<ReviewDataToAdd>(), default))
            .ReturnsAsync(reviewData);
        
        // Act
        await _sut.AddReviewAsync(1, reviewToAdd);
        
        // Assert
        _imageStoreMock.Verify(x => x.StoreImageAsync(imageBytes, default), Times.Once);
        _reviewDataStoreMock.Verify(x => x.AddReviewAsync(It.IsAny<ReviewDataToAdd>(), default), Times.Once);
    }
}