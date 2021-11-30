namespace BurgerBackend.Infrastructure.ImageStore;

internal class AzureBlobImageStore : IImageStore
{
    public async Task<Guid> StoreImageAsync(byte[] bytes, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteImageAsync(Guid imageGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}