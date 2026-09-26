using Microsoft.AspNetCore.Components;
using RentIT.BlazorFrontend.Mappings;
using RentIT.BlazorFrontend.Models.Equipments;
using RentIT.BlazorFrontend.Models.Shared;
using RentIT.UI.Core.HttpClientContracts;
namespace RentIT.BlazorFrontend.Pages.Equipments;

public partial class EquipmentEdit
{
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] private IUserEquipmentHttpClient UserEquipmentHttpClient { get; set; } = default!;
    [Inject] private ICategoriesHttpClient CategoriesHttpClient { get; set; } = default!;

    [Parameter] public Guid EquipmentId { get; set; }
    private UserEquipmentFormModel Model { get; set; } = new();
    private List<BaseDropdownModel> Categories { get; set; } = [];

    protected override async Task OnParametersSetAsync()
    {
        await base.OnInitializedAsync();
        var t2 = await CategoriesHttpClient.GetCategories();
        var t1 = await UserEquipmentHttpClient.GetEquipment(EquipmentId);

        if (t1.IsFailure)
        {
            return;
        }

        var response = t1.Value;
        Model = response.ToUserModel();

        if (t2.IsSuccess)
        {
            Categories = t2.Value
            .Select(x => new BaseDropdownModel() { Id = x.Id, Name = x.Name })
            .ToList();
        }
    }

    private async Task HandleEdit(UserEquipmentFormModel model)
    {
        //validation in future btw
        var request = model.ToUpdateRequest();

        var result = await UserEquipmentHttpClient.PutEquipment(EquipmentId, request);
        if (result.IsSuccess)
            Navigation.NavigateTo("/equipments");
    }
}
