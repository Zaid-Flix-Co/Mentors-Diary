using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.AboutPages;

/// <summary>
/// Class AboutSocialSupport.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class AboutSocialSupport
{
    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// The redirect
    /// </summary>
    private bool _redirect;

    /// <summary>
    /// The redirect path
    /// </summary>
    private string _redirectPath = null!;

    /// <summary>
    /// Redirects to file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    private void RedirectToFile(string filePath)
    {
        _redirect = true;
        _redirectPath = filePath;
        NavigationManager.NavigateTo(filePath, forceLoad: true);
        _redirect = false;
    }
}