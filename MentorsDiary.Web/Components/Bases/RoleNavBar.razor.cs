namespace MentorsDiary.Web.Components.Bases;

public partial class RoleNavBar
{
    private const string NavItemStyle = "nav-item";

    private const string NavItemInStyle = "nav-item-in";

    private string? StyleNavBarForGroups { get; set; } = NavItemStyle;

    private string? StyleNavBarForCurator { get; set; } = NavItemStyle;

    private string? StyleNavBarForStats { get; set; } = NavItemStyle;

    private string? StyleNavBarForEvents { get; set; } = NavItemStyle;

    private string? StyleNavBarForDeputyDirector { get; set; } = NavItemStyle;

    private void SetStyleForGroups()
    {
        StyleNavBarForGroups = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForCurator = $"{NavItemStyle}";
        StyleNavBarForStats = $"{NavItemStyle}";

        StateHasChanged();
    }

    private void SetStyleForCurator()
    {
        StyleNavBarForCurator = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForGroups = $"{NavItemStyle}";
        StyleNavBarForStats = $"{NavItemStyle}";
        StyleNavBarForEvents = $"{NavItemStyle}";
        StyleNavBarForDeputyDirector = $"{NavItemStyle}";

        StateHasChanged();
    }

    private void SetStyleForStats()
    {
        StyleNavBarForStats = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForCurator = $"{NavItemStyle}";
        StyleNavBarForGroups = $"{NavItemStyle}";
        StyleNavBarForEvents = $"{NavItemStyle}";
        StyleNavBarForDeputyDirector = $"{NavItemStyle}";

        StateHasChanged();
    }

    private void SetStyleForEvents()
    {
        StyleNavBarForEvents = $"{NavItemStyle} {NavItemInStyle}";
        StyleNavBarForGroups = $"{NavItemStyle}";
        StyleNavBarForStats = $"{NavItemStyle}";
        StyleNavBarForDeputyDirector = $"{NavItemStyle}";
        StyleNavBarForCurator = $"{NavItemStyle}";

        StateHasChanged();
    }

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