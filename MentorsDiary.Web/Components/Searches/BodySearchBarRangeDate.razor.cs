using AntDesign;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Searches;

/// <summary>
/// Class BodySearchBarRangeDate.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class BodySearchBarRangeDate
{
    /// <summary>
    /// Gets or sets the on selected item changed handler.
    /// </summary>
    /// <value>The on selected item changed handler.</value>
    [Parameter]
    public EventCallback<DateRangeChangedEventArgs<DateTime?[]>?> OnSelectedItemChangedHandler { get; set; }

    /// <summary>
    /// Gets or sets the on clear handler.
    /// </summary>
    /// <value>The on clear handler.</value>
    [Parameter]
    public EventCallback OnClearHandler { get; set; }

    /// <summary>
    /// Gets or sets the on search item changed handler.
    /// </summary>
    /// <value>The on search item changed handler.</value>
    [Parameter]
    public EventCallback<string?> OnSearchItemChangedHandler { get; set; }

    /// <summary>
    /// Gets or sets the text value.
    /// </summary>
    /// <value>The text value.</value>
    private string? TxtValue { get; set; }

    /// <summary>
    /// Gets or sets the selected range date time.
    /// </summary>
    /// <value>The selected range date time.</value>
    private DateTime?[] SelectedRangeDateTime { get; set; } = null!;
}