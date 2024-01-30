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

/// <summary>
/// Class GroupItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupItem
{
    #region PARAMETERS

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
    /// Gets or sets the user service.
    /// </summary>
    /// <value>The user service.</value>
    [Inject]
    private GroupService GroupService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the curator service.
    /// </summary>
    /// <value>The curator service.</value>
    [Inject]
    private CuratorService CuratorService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the division service.
    /// </summary>
    /// <value>The division service.</value>
    [Inject]
    private DivisionService DivisionService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the authentication service.
    /// </summary>
    /// <value>The authentication service.</value>
    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <value>The current user.</value>
    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    /// <summary>
    /// The deputy director
    /// </summary>
    private Group? _group = new();

    /// <summary>
    /// The divisions
    /// </summary>
    /// <value>The divisions.</value>
    private List<Division>? Divisions { get; set; } = new();

    /// <summary>
    /// Gets or sets the clone.
    /// </summary>
    /// <value>The clone.</value>
    private Group? Clone { get; set; } = new();

    /// <summary>
    /// Gets or sets the curators.
    /// </summary>
    /// <value>The curators.</value>
    private List<Curator>? Curators { get; set; } = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "group";

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
    /// The is loading image
    /// </summary>
    private bool _isLoadingImage;

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
    /// Uploads the avatar path.
    /// </summary>
    private async Task UploadAvatarPath()
    {
        if (_group!.ImagePath != null)
        {
            var result = await GroupService?.GetAvatarAsync(_group!.ImagePath)!;
            if (result != null)
                _avatar = result.RequestMessage?.RequestUri?.ToString();
            else
                await MessageService?.Error("Ошибка фотографии")!;
        }
    }

    /// <summary>
    /// Get list as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task GetListAsync()
    {
        _isLoading = true;
        StateHasChanged();

        switch (CurrentUser.Role)
        {
            case EnumRoles.Administrator:
                Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();
                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Curator>()).ToList();
                break;
            case EnumRoles.DeputyDirector:
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

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_group != null)
        {
            _group.Division = null;
            _group.Curator = null;

            if (CurrentUser.Role == EnumRoles.DeputyDirector)
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

    /// <summary>
    /// Handles the <see cref="E:InputFileChange" /> event.
    /// </summary>
    /// <param name="e">The <see cref="InputFileChangeEventArgs" /> instance containing the event data.</param>
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