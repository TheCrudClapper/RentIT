using Microsoft.AspNetCore.Components;
using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.HttpClientContracts;

namespace RentIT.BlazorFrontend.Components.Pages.Equipments;
public partial class EquipmentEdit
{
    [Inject]
    private NavigationManager Navigation { get; set; } = default!;
    [Inject]
    private IUserEquipmentHttpClient UserHttpClient { get; set; } = default!;

    private EquipmentResponse Equipment { get; set; } = default!;
   
    [Parameter]
    public Guid EquipmentId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await UserHttpClient.GetEquipment(EquipmentId);
        if (result.IsFailure)
        {
            return;
        }

        Equipment = result.Value;
    }

    private async Task HandleEdit()
    {

    }
}
