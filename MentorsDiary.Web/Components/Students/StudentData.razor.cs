using AntDesign;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Students;

/// <summary>
/// Class StudentData.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class StudentData
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets the student.
    /// </summary>
    /// <value>The student.</value>
    [Parameter]
    public Student? Student
    {
        get => _student;
        set
        {
            _student = value;
            _ = GetItemAsync();
            _ = UploadAvatarPath();
        }
    }

    /// <summary>
    /// Gets or sets the base URI.
    /// </summary>
    /// <value>The base URI.</value>
    [Parameter]
    public string? BaseUri { get; set; }

    /// <summary>
    /// Gets or sets the student changed.
    /// </summary>
    /// <value>The student changed.</value>
    [Parameter]
    public EventCallback<Student>? StudentChanged { get; set; }

    #endregion

    #region INJECTIONS

    /// <summary>
    /// Gets or sets the parent student service.
    /// </summary>
    /// <value>The parent student service.</value>
    [Inject]
    private ParentStudentService ParentStudentService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the student service.
    /// </summary>
    /// <value>The student service.</value>
    [Inject]
    private StudentService StudentService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private MessageService MessageService { get; set; } = null!;

    /// <summary>
    /// The parents
    /// </summary>
    private List<Parent?>? _parents;

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// The student
    /// </summary>
    private Student? _student = new();

    /// <summary>
    /// The avatar
    /// </summary>
    private string? _avatar;

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        await GetItemAsync();
    }

    /// <summary>
    /// Uploads the avatar path.
    /// </summary>
    private async Task UploadAvatarPath()
    {
        _isLoading = true;
        StateHasChanged();

        if (Student!.ImagePath != null)
        {
            var result = await StudentService?.GetAvatarAsync(Student!.ImagePath)!;
            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString();
            else
                await MessageService?.Error("Ошибка фотографии.")!;
        }

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Get item as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task GetItemAsync()
    {
        _isLoading = true;
        StateHasChanged();

        _parents = (await ParentStudentService.GetAllByFilterAsync(
            new FilterParams
            {
                ColumnName = "StudentId",
                FilterOption = EnumFilterOptions.Contains,
                FilterValue = Student?.Id.ToString()!
            }) ?? Array.Empty<ParentStudent>()).Select(s => s.Parent).ToList();
        
        await UploadAvatarPath();

        _isLoading = false;
        StateHasChanged();
    }
}