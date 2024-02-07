using AntDesign;
using MentorsDiary.Web.Data.Authentication;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Account;

public partial class Login
{
    #region PARAMETERS

    [Parameter]
    public bool Visible { get; set; }

    [Parameter]
    public EventCallback<bool> ChangeVisible { get; set; }

    #endregion

    #region INJECTIONS

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private WebsiteAuthenticator WebsiteAuthenticator { get; set; } = null!;

    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    #endregion

    private LoginFormModel LoginFormModel { get; set; } = new();

    private bool _isLoading;

    private async void TryLogin()
    {
        _isLoading = true;
        StateHasChanged();

        var result = await WebsiteAuthenticator.LoginAsync(LoginFormModel);
        if (result)
        {
            await MessageService.Success("Вход выполнен успешно")!;

            NavigationManager.NavigateTo("/", true);
        }
        else
            await MessageService.Error("Ошибка входа")!;

        _isLoading = false;
        StateHasChanged();
    }
}