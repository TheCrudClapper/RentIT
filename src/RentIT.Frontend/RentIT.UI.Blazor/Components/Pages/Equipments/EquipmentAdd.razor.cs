using Microsoft.AspNetCore.Components;
using RentIT.BlazorFrontend.Models.Equipments;
using RentIT.BlazorFrontend.Models.Shared;
using RentIT.UI.Core.HttpClientContracts;

namespace RentIT.BlazorFrontend.Components.Pages.Equipments;

public partial class EquipmentAdd
{
    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
    [Inject]
    private IUserEquipmentHttpClient _userHttpClient { get; set; } = default!;

    [SupplyParameterFromForm]
    private EquipmentModel Model { get; set; } = new();
    private List<SelectItem> Categories { get; set; } = [new() { Id = Guid.NewGuid(), Name = "Sex with pedals" }];
    private List<(int, string)> Statuses { get; set; } = [ new (1, "Rented"), new(2, "Nigga")];
    private async Task HandleAdd()
    {

    }
 
}
