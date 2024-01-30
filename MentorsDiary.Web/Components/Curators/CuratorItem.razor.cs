using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MentorsDiary.Web.Components.Curators;

/// <summary>
/// Class CuratorItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class CuratorItem
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets the curator identifier.
    /// </summary>
    /// <value>The curator identifier.</value>
    [Parameter]
    public int CuratorId { get; set; }

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
    public UserService UserService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the curator service.
    /// </summary>
    /// <value>The curator service.</value>
    [Inject]
    public CuratorService CuratorService { get; set; } = null!;

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
    private MessageService MessageService { get; set; } = null!;

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
    /// The curator
    /// </summary>
    private Curator? _curator = new() { User = new User { Division = new Division() } };

    /// <summary>
    /// The divisions
    /// </summary>
    /// <value>The divisions.</value>
    private List<Division>? Divisions { get; set; } = new();

    /// <summary>
    /// Gets or sets the selected division.
    /// </summary>
    /// <value>The selected division.</value>
    private Division? SelectedDivision { get; set; } = new();

    /// <summary>
    /// Gets or sets the clone.
    /// </summary>
    /// <value>The clone.</value>
    private Curator? Clone { get; set; } = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "curator";

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
        if (_curator!.ImagePath != null)
        {
            var result = await UserService?.GetAvatarAsync(_curator!.ImagePath)!;
            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString();
            else
                await MessageService?.Error("Ошибка фотографии.")!;
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

        Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();
        _curator = await CuratorService.GetIdAsync(CuratorId);

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

        if (_curator != null)
        {
            if (_curator.User != null)
            {
                _curator.Name = _curator.User.Name;
                _curator.User.Role = EnumRoles.Curator;
                _curator.User.Division = null;

                if(_isLoadingImage)
                    await UploadAvatar();
            }

            var responseUpdateCurator = await CuratorService.UpdateAsync(_curator);
            if (_curator.User != null)
            {
                var responseUpdateUser = await UserService.UpdateAsync(_curator.User);

                if (responseUpdateCurator.IsSuccessStatusCode && responseUpdateUser.IsSuccessStatusCode)
                    await MessageService.Success($"Пользователь {_curator.Name} успешно добавлен.");
                else
                    await MessageService.Error($"{responseUpdateCurator.ReasonPhrase}\n{responseUpdateUser.ReasonPhrase}");
            }
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo("/curator");
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

        var response = await UserService?.UploadAvatarAsync(content)!;

        if (response.IsSuccessStatusCode)
        {
            _curator!.ImagePath = fileName;
            Clone!.ImagePath = fileName;

            var result = await UserService.GetAvatarAsync(_curator!.ImagePath);
            _avatar = result.RequestMessage?.RequestUri?.ToString();
        }
        else
            await MessageService.Error($"{response.ReasonPhrase}");

        _isLoadingImage = false;
    }

    /// <summary>
    /// Called when [selected item changed handler].
    /// </summary>
    /// <param name="value">The value.</param>
    private void OnSelectedItemChangedHandler(Division value)
    {
        SelectedDivision = value;
        StateHasChanged();
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