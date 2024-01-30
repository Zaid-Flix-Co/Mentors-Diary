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

/// <summary>
/// Class StudentItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class StudentItem
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets the student identifier.
    /// </summary>
    /// <value>The student identifier.</value>
    [Parameter]
    public int StudentId { get; set; }

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
    /// Gets or sets the student service.
    /// </summary>
    /// <value>The student service.</value>
    [Inject]
    public StudentService StudentService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private MessageService MessageService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the parent student service.
    /// </summary>
    /// <value>The parent student service.</value>
    [Inject]
    private ParentStudentService ParentStudentService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the parent service.
    /// </summary>
    /// <value>The parent service.</value>
    [Inject]
    private ParentService ParentService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// The student
    /// </summary>
    private Student? _student = new();

    /// <summary>
    /// The parents
    /// </summary>
    private List<Parent?> _parents = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => $"/group-page/{GroupId}";

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// The avatar
    /// </summary>
    private string? _avatar;

    /// <summary>
    /// The new avatar
    /// </summary>
    private string? _newAvatar;

    /// <summary>
    /// The resized image
    /// </summary>
    private IBrowserFile? _resizedImage;

    /// <summary>
    /// Gets or sets the clone.
    /// </summary>
    /// <value>The clone.</value>
    private Student? Clone { get; set; } = new();

    /// <summary>
    /// The is image loading
    /// </summary>
    private bool _isImageLoading;

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
        if (_student!.ImagePath != null)
        {
            var result = await StudentService?.GetAvatarAsync(_student!.ImagePath)!;
            if (result != null)
                _avatar = result.RequestMessage?.RequestUri?.ToString();
            else
                await MessageService?.Error("Ошибка фотографии.")!;
        }
    }

    /// <summary>
    /// Get item as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task GetItemAsync()
    {
        _isLoading = true;
        StateHasChanged();

        _student = await StudentService.GetIdAsync(StudentId);
        _parents = (await ParentStudentService.GetAllByFilterAsync(new FilterParams
        {
            ColumnName = "StudentId",
            FilterOption = EnumFilterOptions.Contains,
            FilterValue = _student?.Id.ToString()!
        }) ?? Array.Empty<ParentStudent>()).Select(p => p.Parent).ToList();

        await UploadAvatarPath();

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

        if (_student != null)
        {
            _student.GroupId = GroupId;

            if(_isImageLoading)
                await UploadAvatar();

            var responseUpdateStudent = await StudentService.UpdateAsync(_student);

            var responseUpdateMother = await ParentService.UpdateAsync(_parents[0]!);
            var responseUpdateFather = await ParentService.UpdateAsync(_parents[1]!);
            
            if (responseUpdateStudent.IsSuccessStatusCode && responseUpdateMother.IsSuccessStatusCode &&
                responseUpdateFather.IsSuccessStatusCode)
                await MessageService.Success($"Студент {_student.Name} успешно добавлен.");
            else
                await MessageService.Error(responseUpdateStudent.ReasonPhrase);
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo(NavigateToUri);
    }

    /// <summary>
    /// Uploads the avatar.
    /// </summary>
    private async Task UploadAvatar()
    {
        using var content = new MultipartFormDataContent();
        var fileName = Path.GetRandomFileName();

        content.Add(
            content: new StreamContent(_resizedImage?.OpenReadStream() ?? Stream.Null),
            name: "\"files\"",
            fileName: fileName);

        var response = await StudentService?.UploadAvatarAsync(content)!;

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

    /// <summary>
    /// Handles the <see cref="E:InputFileChange" /> event.
    /// </summary>
    /// <param name="e">The <see cref="InputFileChangeEventArgs" /> instance containing the event data.</param>
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