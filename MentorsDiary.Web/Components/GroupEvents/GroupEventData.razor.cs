using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.GroupEvents;

/// <summary>
/// Class GroupEventData.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupEventData
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets the group event.
    /// </summary>
    /// <value>The group event.</value>
    [Parameter]
    public required GroupEvent GroupEvent { get; set; }

    /// <summary>
    /// Gets or sets the visible.
    /// </summary>
    /// <value>The visible.</value>
    [Parameter]
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the visible changed.
    /// </summary>
    /// <value>The visible changed.</value>
    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    #endregion

    #region INJECTIONS

    /// <summary>
    /// Gets the group event student service.
    /// </summary>
    /// <value>The group event student service.</value>
    [Inject]
    private GroupEventStudentService GroupEventStudentService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// The students
    /// </summary>
    private List<Student> _students = new();

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;System.Threading.Tasks.Task&gt; representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        await GetItemsAsync();
    }

    /// <summary>
    /// Get items as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;System.Threading.Tasks.Task&gt; representing the asynchronous operation.</returns>
    private async Task GetItemsAsync()
    {
        _isLoading = true;
        StateHasChanged();

        _students = (await GroupEventStudentService.GetAllByFilterAsync(new FilterParams
        {
            ColumnName = "GroupEventId",
            FilterOption = EnumFilterOptions.Contains,
            FilterValue = GroupEvent.Id.ToString()
        }) ?? Array.Empty<GroupEventStudent>()).Select(s => s.Student).ToList()!;

        _isLoading = false;
        StateHasChanged();
    }
}