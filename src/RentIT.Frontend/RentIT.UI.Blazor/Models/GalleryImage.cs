using Microsoft.AspNetCore.Components.Forms;

namespace RentIT.BlazorFrontend.Models;
public class GalleryImage
{
    public Guid? Id { get; set; }
    public string Url { get; set; } = null!;
    public bool IsCover { get; set; }
    public IBrowserFile? PendingFile { get; set; }
}
