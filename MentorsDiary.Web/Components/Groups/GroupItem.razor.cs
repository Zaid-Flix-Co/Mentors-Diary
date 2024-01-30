using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MentorsDiary.Web.Components.Groups;

public partial class GroupItem
{
    #region PARAMETERS

    [Parameter]
    public int GroupId { get; set; }

    #endregion

    #region INJECTIONS

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private GroupService GroupService { get; set; } = null!;

    [Inject]
    private CuratorService CuratorService { get; set; } = null!;

    [Inject]
    private DivisionService DivisionService { get; set; } = null!;

    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    private Group? _group = new();

    private List<Division>? Divisions { get; set; } = new();

    private Group? Clone { get; set; } = new();

    private List<Curator>? Curators { get; set; } = new();

    private static string NavigateToUri => "group";

    private bool _isLoading;

    private string? _avatar;

    private string? _newAvatar;

    private IBrowserFile? _resizedImage;

    private bool _isLoadingImage;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await GetListAsync();
    }

    /// <summary>
    /// Uploads the avatar path.
    /// </summary>
    private async Task UploadAvatarPath()
    {
        if (_group!.ImagePath != null)
        {
            var result = await GroupService.GetAvatarAsync(_group!.ImagePath)!;

            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString();
            else
                Console.WriteLine("Ошибка фотографии");
        }
    }

    private async Task GetListAsync()
    {
        _isLoading = true;
        StateHasChanged();

        switch (CurrentUser.Role)
        {
            case Roles.Administrator:
                Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();
                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Curator>()).ToList();
                break;
            case Roles.DeputyDirector:
                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Curator>())
                    .Where(c => c.User?.DivisionId == CurrentUser.DivisionId).ToList();
                break;
        }

        _group = await GroupService.GetIdAsync(GroupId);

        await UploadAvatarPath();

        _isLoading = false;
        StateHasChanged();
    }

    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_group != null)
        {
            _group.Division = null;
            _group.Curator = null;

            if (CurrentUser.Role == Roles.DeputyDirector)
                _group.DivisionId = CurrentUser.DivisionId;

            if (_isLoadingImage)
                await UploadAvatar();

            var response = await GroupService.UpdateAsync(_group);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Группа {_group.Name} успешно добавлена.");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo($"/{NavigateToUri}");
    }

    private async Task UploadAvatar()
    {
        using var content = new MultipartFormDataContent();
        var fileName = Path.GetRandomFileName();

        content.Add(
            content: new StreamContent(_resizedImage?.OpenReadStream() ?? Stream.Null),
            name: "\"files\"",
            fileName: fileName);

        var response = await GroupService?.UploadAvatarAsync(content)!;

        if (response.IsSuccessStatusCode)
        {
            _group!.ImagePath = fileName;
            Clone!.ImagePath = fileName;

            var result = await GroupService.GetAvatarAsync(_group!.ImagePath);
            _avatar = result.RequestMessage?.RequestUri?.ToString();
        }
        else
            await MessageService.Error("Upload failed.");

        _isLoadingImage = true;
    }

    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        _isLoadingImage = true;
        var imageFile = e.File;
        if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
        {
            await MessageService.Error("You can only upload JPG/PNG file!");
        }
        else
        {
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