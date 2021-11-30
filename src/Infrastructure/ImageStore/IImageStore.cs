namespace BurgerBackend.Infrastructure.ImageStore;

internal interface IImageStore
{
    Task<Guid> StoreImageAsync(byte[] bytes, CancellationToken cancellationToken = default);
    Task DeleteImageAsync(Guid imageGuid, CancellationToken cancellationToken = default);
}