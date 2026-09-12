using Microsoft.AspNetCore.Components;
using RentIT.BlazorFrontend.Models.Equipments;
using RentIT.BlazorFrontend.Models.Shared;
using RentIT.UI.Core.HttpClientContracts;

namespace RentIT.BlazorFrontend.Pages.Equipments;

public partial class EquipmentEdit
{
    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
    [Inject]
    private IUserEquipmentHttpClient _userHttpClient { get; set; } = default!;

    [SupplyParameterFromForm]
    private EquipmentModel Model { get; set; } = new();
    private List<SelectItem> Categories { get; set; } = [new() { Id = Guid.NewGuid(), Name = "Sex with pedals" }];
    private List<(int, string)> Statuses { get; set; } = [new(1, "Rented"), new(2, "Nigga")];

    [Parameter]
    public Guid EquipmentId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await _userHttpClient.GetEquipment(EquipmentId);
        if (result.IsFailure)
        {
            return;
        }

        var response = result.Value;
        Model = new()
        {
            Name = response.Name,
            Notes = response.Notes,
            RentalPricePerDay = response.RentalPricePerDay,
            SerialNumber = response.SerialNumber,
        };
    }

    private async Task HandleEdit()
    {

    }
}
