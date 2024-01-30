using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Web.Data.Services.Bases;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Searches;

/// <summary>
/// Class BodySearchBar.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <typeparam name="TValue">The type of the t value.</typeparam>
/// <typeparam name="TService">The type of the t service.</typeparam>
/// <seealso cref="ComponentBase" />
public partial class BodySearchBar<TValue, TService>
    where TValue : class, IHaveId, IHaveName, new()
    where TService : BaseService<TValue>
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets the on selected item changed handler.
    /// </summary>
    /// <value>The on selected item changed handler.</value>
    [Parameter]
    public EventCallback<TValue?> OnSelectedItemChangedHandler { get; set; }

    /// <summary>
    /// Gets or sets the on search item changed handler.
    /// </summary>
    /// <value>The on search item changed handler.</value>
    [Parameter]
    public EventCallback<string?> OnSearchItemChangedHandler { get; set; }

    #endregion

    #region INJECTIONS

    /// <summary>
    /// Gets or sets the division service.
    /// </summary>
    /// <value>The division service.</value>
    [Inject]
    private TService? Service { get; set; }

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Gets or sets the values.
    /// </summary>
    /// <value>The values.</value>
    private List<TValue>? Values { get; set; }

    /// <summary>
    /// Gets or sets the selected value.
    /// </summary>
    /// <value>The selected value.</value>
    private string? SelectedValue { get; set; }

    /// <summary>
    /// Gets or sets the text value.
    /// </summary>
    /// <value>The text value.</value>
    public string TxtValue { get; set; } = string.Empty;

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        var ret = await Service?.GetAllAsync()!;

        Values = new List<TValue>();
        Values?.Add(new TValue
        {
            Id = -1, 
            Name = "Все"
        });
        Values?.AddRange((ret ?? Array.Empty<TValue>()).ToList());
    }
    
    /// <summary>
    /// Called when [press enter handler].
    /// </summary>
    private async Task OnPressEnterHandler()
    {
        if (OnSearchItemChangedHandler.HasDelegate)
            await OnSearchItemChangedHandler.InvokeAsync(TxtValue);
    }
}