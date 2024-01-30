using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Bases;

/// <summary>
/// Class RoleNavBar.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class RoleNavBar
{
    /// <summary>
    /// The nav item style
    /// </summary>
    private const string NavItemStyle = "nav-item";

    /// <summary>
    /// The nav item in style
    /// </summary>
    private const string NavItemInStyle = "nav-item-in";

    /// <summary>
    /// Gets or sets the style nav bar for groups.
    /// </summary>
    /// <value>The style nav bar for groups.</value>
    private string? StyleNavBarForGroups { get; set; } = NavItemStyle;

    /// <summary>
    /// Gets or sets the style nav bar for curator.
    /// </summary>
    /// <value>The style nav bar for curator.</value>
    private string? StyleNavBarForCurator { get; set; } = NavItemStyle;

    /// <summary>
    /// Gets or sets the style nav bar for stats.
    /// </summary>
    /// <value>The style nav bar for stats.</value>
    private string? StyleNavBarForStats { get; set; } = NavItemStyle;

    /// <summary>
    /// Gets or sets the style nav bar for events.
    /// </summary>
    /// <value>The style nav bar for events.</value>
    private string? StyleNavBarForEvents { get; set; } = NavItemStyle;

    /// <summary>
    /// Gets or sets the style nav bar for deputy director.
    /// </summary>
    /// <value>The style nav bar for deputy director.</value>
    private string? StyleNavBarForDeputyDirector { get; set; } = NavItemStyle;

    /// <summary>
    /// Sets the style for groups.
    /// </summary>
    private void SetStyleForGroups()
    {
        StyleNavBarForGroups = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForCurator = $"{NavItemStyle}";
        StyleNavBarForStats = $"{NavItemStyle}";

        StateHasChanged();
    }

    /// <summary>
    /// Sets the style for curator.
    /// </summary>
    private void SetStyleForCurator()
    {
        StyleNavBarForCurator = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForGroups = $"{NavItemStyle}";
        StyleNavBarForStats = $"{NavItemStyle}";
        StyleNavBarForEvents = $"{NavItemStyle}";
        StyleNavBarForDeputyDirector = $"{NavItemStyle}";

        StateHasChanged();
    }

    /// <summary>
    /// Sets the style for stats.
    /// </summary>
    private void SetStyleForStats()
    {
        StyleNavBarForStats = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForCurator = $"{NavItemStyle}";
        StyleNavBarForGroups = $"{NavItemStyle}";
        StyleNavBarForEvents = $"{NavItemStyle}";
        StyleNavBarForDeputyDirector = $"{NavItemStyle}";

        StateHasChanged();
    }

    /// <summary>
    /// Sets the style for events.
    /// </summary>
    private void SetStyleForEvents()
    {
        StyleNavBarForEvents = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForGroups = $"{NavItemStyle}";
        StyleNavBarForStats = $"{NavItemStyle}";
        StyleNavBarForDeputyDirector = $"{NavItemStyle}";
        StyleNavBarForCurator = $"{NavItemStyle}";

        StateHasChanged();
    }

    /// <summary>
    /// Sets the style for deputy director.
    /// </summary>
    private void SetStyleForDeputyDirector()
    {
        StyleNavBarForDeputyDirector = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForCurator = $"{NavItemStyle}";
        StyleNavBarForGroups = $"{NavItemStyle}";
        StyleNavBarForEvents = $"{NavItemStyle}";
        StyleNavBarForStats = $"{NavItemStyle}";

        StateHasChanged();
    }
}