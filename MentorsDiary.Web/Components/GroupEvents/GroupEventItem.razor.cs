using AntDesign;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.GroupEvents;

/// <summary>
/// Class GroupEventItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupEventItem
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets the group event identifier.
    /// </summary>
    /// <value>The group event identifier.</value>
    [Parameter]
    public int GroupEventId { get; set; }

    /// <summary>
    /// Gets or sets the group identifier.
    /// </summary>
    /// <value>The group identifier.</value>
    [Parameter]
    public int GroupId { get; set; }

    #endregion

    #region INJECTIONS

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// Gets or sets the group event service.
    /// </summary>
    /// <value>The group event service.</value>
    [Inject]
    private GroupEventService GroupEventService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the event service.
    /// </summary>
    /// <value>The event service.</value>
    [Inject]
    private EventService EventService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private MessageService MessageService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the student service.
    /// </summary>
    /// <value>The student service.</value>
    [Inject]
    private StudentService StudentService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the group event student service.
    /// </summary>
    /// <value>The group event student service.</value>
    [Inject]
    private GroupEventStudentService GroupEventStudentService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// The student
    /// </summary>
    private GroupEvent? _groupEvent = new();

    /// <summary>
    /// Gets or sets the events.
    /// </summary>
    /// <value>The events.</value>
    private IEnumerable<Application.Entities.Events.Domains.Event> Events { get; set; } = new List<Application.Entities.Events.Domains.Event>();

    /// <summary>
    /// Gets or sets the students.
    /// </summary>
    /// <value>The students.</value>
    private IEnumerable<Student> Students { get; set; } = new List<Student>();

    /// <summary>
    /// Gets or sets the group event student.
    /// </summary>
    /// <value>The group event student.</value>
    private IEnumerable<GroupEventStudent> GroupEventStudent { get; set; } = new List<GroupEventStudent>();

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// Gets or sets the selected students identifier.
    /// </summary>
    /// <value>The selected students identifier.</value>
    private IEnumerable<int> SelectedStudentsId { get; set; } = new List<int>();

    /// <summary>
    /// Gets or sets the selected students identifier list.
    /// </summary>
    /// <value>The selected students identifier list.</value>
    private List<int> SelectedStudentsIdList { get; set; } = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => $"/group-page/{GroupId}";

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        await GetListAsync();
    }

    /// <summary>
    /// Get list as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task GetListAsync()
    {
        _isLoading = true;
        StateHasChanged();

        _groupEvent = await GroupEventService.GetIdAsync(GroupEventId);

        Events = (await EventService.GetAllAsync() ?? Array.Empty<Application.Entities.Events.Domains.Event>()).ToList();
        Students = (await StudentService.GetAllAsync() ?? Array.Empty<Student>()).Where(s => s.GroupId == GroupId);
        GroupEventStudent = (await GroupEventStudentService.GetAllAsync() ?? Array.Empty<GroupEventStudent>()).Where(s => s.GroupEventId == GroupEventId);

        foreach (var groupEventStudent in GroupEventStudent)
        {
            if(groupEventStudent.GroupEventId == GroupEventId)
                SelectedStudentsIdList.Add(groupEventStudent.StudentId);
        }

        SelectedStudentsId = SelectedStudentsIdList.AsEnumerable();

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_groupEvent != null)
        {
            _groupEvent.Event = null;
            _groupEvent.GroupId = GroupId;

            var response = await GroupEventService.UpdateAsync(_groupEvent);

            var students = (await StudentService.GetAllAsync() ?? Array.Empty<Student>()).ToList()
                .Where(c => SelectedStudentsId.Any(s => s == c.Id)).ToList();

            var studentsInGroupEvent = new List<GroupEventStudent>();
            for (var index = 0; index < students.Count; index++)
            {
                studentsInGroupEvent.Add(new GroupEventStudent());
                studentsInGroupEvent[index].GroupEventId = GroupEventId;
                studentsInGroupEvent[index].StudentId = students[index].Id;
            }

            HttpResponseMessage responseGroupEventStudent;
            if (studentsInGroupEvent.Count == 0)
            {
                studentsInGroupEvent.Add(new GroupEventStudent { GroupEventId = GroupEventId });
                responseGroupEventStudent = await GroupEventStudentService.AddStudentsInGroupEvent(studentsInGroupEvent);
            }
            else
                responseGroupEventStudent = await GroupEventStudentService.AddStudentsInGroupEvent(studentsInGroupEvent);

            if (response.IsSuccessStatusCode && responseGroupEventStudent.IsSuccessStatusCode)
                await MessageService.Success($"Событие {_groupEvent.Name} успешно добавлено.");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo(NavigateToUri);
    }

    /// <summary>
    /// Called when [selected items changed handler].
    /// </summary>
    /// <param name="students">The students.</param>
    private void OnSelectedItemsChangedHandler(IEnumerable<Student> students)
    {
        if (_groupEvent != null)
            _groupEvent.CountParticipants = students.Count();
    }
}