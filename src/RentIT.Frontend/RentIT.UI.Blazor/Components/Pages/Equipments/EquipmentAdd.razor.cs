using Microsoft.AspNetCore.Components;
using RentIT.BlazorFrontend.Models.Equipments;
using RentIT.UI.Core.HttpClientContracts;

namespace RentIT.BlazorFrontend.Components.Pages.Equipments;

public partial class EquipmentAdd
{
    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
    [Inject]
    private IUserEquipmentHttpClient UserHttpClient { get; set; } = default!;

    [SupplyParameterFromForm]
    private EquipmentAddModel Model { get; set; } = new();

    private async Task HandleAdd()
    {

    }
 
}
