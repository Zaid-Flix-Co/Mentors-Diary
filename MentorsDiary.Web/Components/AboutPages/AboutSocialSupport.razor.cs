using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.AboutPages;

public partial class AboutSocialSupport
{
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

    private bool _redirect;

    private string _redirectPath = null!;

    private void RedirectToFile(string filePath)
    {
        _redirect = true;
        _redirectPath = filePath;

        NavigationManager.NavigateTo(filePath, forceLoad: true);

        _redirect = false;
    }
}