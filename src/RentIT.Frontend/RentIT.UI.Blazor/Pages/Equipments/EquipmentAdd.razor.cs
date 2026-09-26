using Microsoft.AspNetCore.Components;
using RentIT.BlazorFrontend.Mappings;
using RentIT.BlazorFrontend.Models.Equipments;
using RentIT.BlazorFrontend.Models.Shared;
using RentIT.UI.Core.HttpClientContracts;

namespace RentIT.BlazorFrontend.Pages.Equipments;

public partial class EquipmentAdd
{
    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] private IUserEquipmentHttpClient UserEquipmentHttpClient { get; set; } = default!;
    [Inject] private ICategoriesHttpClient CategoriesHttpClient { get; set; } = default!;
    public List<BaseDropdownModel> Categories { get; set; } = [];
    private UserEquipmentFormModel Model { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var result = await CategoriesHttpClient.GetCategories();
        if (result.IsFailure)
        {

        }
        Categories = result.Value
            .Select(x => new BaseDropdownModel() { Id = x.Id, Name = x.Name })
            .ToList();
    }

    private async Task HandleAdd(UserEquipmentFormModel model)
    {
        //validation in future btw
        var request = model.ToAddRequest();


        var result = await UserEquipmentHttpClient.PostEquipment(request);
        if (result.IsSuccess)
            Navigation.NavigateTo("/equipments");
    }
    
}
