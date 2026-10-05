using EquipmentService.API.DTO.Public;
using EquipmentService.Core.DTO.Images;

namespace EquipmentService.API.Mappings;

public static class ImageRequestMapper
{
    public async static Task<ImageRequest> MapAsync(this ImageRequestExternal dto)
    {
        byte[] image;
        if (dto.Image is null || dto.Image.Length == 0)
        {
            image = [];
        }
        else
        {
            using var stream = new MemoryStream();
            {
                await dto.Image.CopyToAsync(stream);
            }
            image = stream.ToArray();
        }

        return new ImageRequest(dto.Id, dto.IsCover, image);
    }
}
