using Microsoft.AspNetCore.Components;
using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.HttpClientContracts;

namespace RentIT.BlazorFrontend.Components.Pages.Equipments;

public partial class EquipmentList
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private IUserEquipmentHttpClient UserHttpClient { get; set; } = default!;

    private List<UserEquipmentListResponse> Equipments = [];

    protected override async Task OnInitializedAsync()
    {
        await Refresh();
    }

    private async Task HandleDelete(UserEquipmentListResponse equipment)
    {
        var result = await UserHttpClient.DeleteEquipment(equipment.Id);
        if (result.IsFailure)
        {

        }
        else
        {
            Equipments.Remove(equipment);
            await Refresh();
        }
    }

    private async Task HandleEdit(Guid id)
    {
        NavigationManager.NavigateTo($"/equipments/{id}/edit");
    }

    private async Task Refresh()
    {
        var result = await UserHttpClient.GetUserEquipmentsList();
        Equipments = result.Value.ToList();
    }
}
