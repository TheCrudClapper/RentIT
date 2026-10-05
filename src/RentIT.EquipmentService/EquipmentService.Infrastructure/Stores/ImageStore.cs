using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.Enums;
namespace EquipmentService.Infrastructure.Stores;

public interface IImageStorage
{
    Task<Result<string>> SaveImagesAsync(IEnumerable<byte[]> images, ImageType type);
    Task DeleteImages(IEnumerable<string> imagePaths);
}

public class ImageStore : IImageStorage
{
    private Dictionary<ImageType, string> _fileLocations = new Dictionary<ImageType, string>
    {
        [ImageType.Equipment] = "path/to/jpeg",
        [ImageType.Listing] = "path/to/png"
    };

    public Task DeleteImages(IEnumerable<string> imagePaths)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> SaveImagesAsync(IEnumerable<byte[]> images, ImageType type)
    {
        throw new NotImplementedException();
    }
}