using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Authentication;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Components.Bases;

public partial class MainHeader
{
    #region INJECTIONS

    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] 
    private UserService UserService { get; set; } = null!;

    [Inject]
    private ProtectedLocalStorage ProtectedLocalStorage { get; set; } = null!;

    [Inject]
    private WebsiteAuthenticator WebsiteAuthenticator { get; set; } = null!;

    #endregion

    private User? CurrentUser { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            try
            {
                var value = (await ProtectedLocalStorage.GetAsync<string>("identity")).Value;
                if (value != null)
                    CurrentUser = JsonConvert.DeserializeObject<User>(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public void NavigateToLoginWindow()
    {
        NavigationManager.NavigateTo("/login");
    }

    private void NavigateToHome()
    {
        NavigationManager.NavigateTo("/");
    }

    private async Task UpdatePassword()
    {
        var list = (await UserService.GetAllAsync() ?? Array.Empty<User>()).ToList();
        await UserService.CreateApplicationUsersAsync(list);
    }

    private async Task TryLogout()
    {
        await WebsiteAuthenticator.LogoutAsync();

        NavigationManager.NavigateTo("/", true);
    }
}