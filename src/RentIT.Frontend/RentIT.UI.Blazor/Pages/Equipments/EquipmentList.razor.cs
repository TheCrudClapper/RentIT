using Microsoft.AspNetCore.Components;
using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.HttpClientContracts;

namespace RentIT.BlazorFrontend.Pages.Equipments;

public partial class EquipmentList
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IUserEquipmentHttpClient UserEquipmentHttpClient { get; set; } = default!;
    private List<UserEquipmentListResponse> Equipments { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await RefreshAsync();
    }

    private async Task HandleDelete(UserEquipmentListResponse equipment)
    {
        var result = await UserEquipmentHttpClient.DeleteEquipment(equipment.Id);
        //if (result.IsSuccess)
        //{
            Equipments.Remove(equipment);
            //await RefreshAsync();
            //StateHasChanged();
        
    }

    private async Task HandleEdit(Guid id)
    {
        NavigationManager.NavigateTo($"/equipments/{id}/edit");
    }

    private async Task RefreshAsync()
    {
        var result = await UserEquipmentHttpClient.GetUserEquipmentsList();
        Equipments = result.Value.ToList();
    }
}
