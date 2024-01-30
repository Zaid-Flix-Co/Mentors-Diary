using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Students;

public partial class StudentData
{
    #region PARAMETERS

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

    [Parameter]
    public string? BaseUri { get; set; }

    [Parameter]
    public EventCallback<Student>? StudentChanged { get; set; }

    #endregion

    #region INJECTIONS

    [Inject]
    private ParentStudentService ParentStudentService { get; set; } = null!;

    [Inject]
    private StudentService StudentService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private List<Parent?>? _parents;

    private bool IsLoading { get; set; }

    private Student? _student = new();

    private string? _avatar;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await GetItemAsync();
    }

    private async Task UploadAvatarPath()
    {
        IsLoading = true;
        StateHasChanged();

        if (Student!.ImagePath != null)
        {
            var result = await StudentService.GetAvatarAsync(Student!.ImagePath)!;

            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString();
            else
                Console.WriteLine("Ошибка фотографии");
        }

        IsLoading = false;
        StateHasChanged();
    }

    private async Task GetItemAsync()
    {
        IsLoading = true;
        StateHasChanged();

        _parents = (await ParentStudentService.GetAllByFilterAsync(
            new FilterParams
            {
                ColumnName = "StudentId",
                FilterOption = FilterOptions.Contains,
                FilterValue = Student?.Id.ToString()!
            }) ?? Array.Empty<ParentStudent>()).Select(s => s.Parent).ToList();
        
        await UploadAvatarPath();

        IsLoading = false;
        StateHasChanged();
    }
}