using AntDesign;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MentorsDiary.Web.Components.Students;

public partial class StudentItem
{
    #region PARAMETERS

    [Parameter]
    public int StudentId { get; set; }

    [Parameter]
    public int GroupId { get; set; }

    #endregion

    #region INJECTIONS

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public StudentService StudentService { get; set; } = null!;

    [Inject]
    private MessageService MessageService { get; set; } = null!;

    [Inject]
    private ParentStudentService ParentStudentService { get; set; } = null!;

    [Inject]
    private ParentService ParentService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    private Student? _student = new();

    private List<Parent?> _parents = new();

    private string NavigateToUri => $"/group-page/{GroupId}";

    private bool _isLoading;

    private string? _avatar;

    private string? _newAvatar;

    private IBrowserFile? _resizedImage;

    private Student? Clone { get; set; } = new();

    private bool _isImageLoading;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await GetItemAsync();
    }

    private async Task UploadAvatarPath()
    {
        if (_student!.ImagePath != null)
        {
            var result = await StudentService.GetAvatarAsync(_student!.ImagePath);

            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString();
            else
                Console.WriteLine("Ошибка фотографии");
        }
    }

    private async Task GetItemAsync()
    {
        _isLoading = true;
        StateHasChanged();

        _student = await StudentService.GetIdAsync(StudentId);
        _parents = (await ParentStudentService.GetAllByFilterAsync(new FilterParams
        {
            ColumnName = "StudentId",
            FilterOption = FilterOptions.Contains,
            FilterValue = _student?.Id.ToString()!
        }) ?? Array.Empty<ParentStudent>()).Select(p => p.Parent).ToList();

        await UploadAvatarPath();

        _isLoading = false;
        StateHasChanged();
    }

    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_student != null)
        {
            _student.GroupId = GroupId;

            if(_isImageLoading)
                await UploadAvatar();

            var responseUpdateStudent = await StudentService.UpdateAsync(_student);
            var responseUpdateMother = await ParentService.UpdateAsync(_parents[0]!);
            var responseUpdateFather = await ParentService.UpdateAsync(_parents[1]!);
            
            if (responseUpdateStudent.IsSuccessStatusCode && responseUpdateMother.IsSuccessStatusCode && responseUpdateFather.IsSuccessStatusCode)
                await MessageService.Success($"Студент {_student.Name} успешно добавлен.");
            else
                await MessageService.Error(responseUpdateStudent.ReasonPhrase);
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo(NavigateToUri);
    }

    private async Task UploadAvatar()
    {
        using var content = new MultipartFormDataContent();
        var fileName = Path.GetRandomFileName();

        content.Add(
            content: new StreamContent(_resizedImage?.OpenReadStream() ?? Stream.Null),
            name: "\"files\"",
            fileName: fileName);

        var response = await StudentService.UploadAvatarAsync(content)!;

        if (response.IsSuccessStatusCode)
        {
            _student!.ImagePath = fileName;
            Clone!.ImagePath = fileName;

            var result = await StudentService.GetAvatarAsync(_student!.ImagePath);
            _avatar = result.RequestMessage?.RequestUri?.ToString();
        }
        else
            await MessageService.Error("Upload failed.");

        _isImageLoading = false;
    }

    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        var imageFile = e.File;
        if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
        {
            await MessageService.Error("You can only upload JPG/PNG file!");
        }
        else
        {
            _isImageLoading = true;

            _resizedImage = await imageFile.RequestImageFileAsync("image/png", 500, 500);

            var ms = new MemoryStream();
            await _resizedImage.OpenReadStream().CopyToAsync(ms);
            var bytes = ms.ToArray();

            var b64 = Convert.ToBase64String(bytes);

            _newAvatar = "data:image/png;base64," + b64;
            _avatar = _newAvatar;
        }

        StateHasChanged();
    }
}