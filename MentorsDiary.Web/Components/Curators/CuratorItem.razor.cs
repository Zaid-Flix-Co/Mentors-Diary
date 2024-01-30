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

public partial class CuratorItem
{
    #region PARAMETERS

    [Parameter]
    public int CuratorId { get; set; }

    #endregion

    #region INJECTIONS

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public UserService UserService { get; set; } = null!;

    [Inject]
    public CuratorService CuratorService { get; set; } = null!;

    [Inject]
    private DivisionService DivisionService { get; set; } = null!;

    [Inject]
    private MessageService MessageService { get; set; } = null!;

    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    private Curator? _curator = new() { User = new User { Division = new Division() } };

    private List<Division>? Divisions { get; set; } = new();

    private Division? SelectedDivision { get; set; } = new();

    private Curator? Clone { get; set; } = new();

    private static string NavigateToUri => "curator";

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

    private async Task UploadAvatarPath()
    {
        if (_curator!.ImagePath != null)
        {
            var result = await UserService?.GetAvatarAsync(_curator!.ImagePath)!;

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

        Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();
        _curator = await CuratorService.GetIdAsync(CuratorId);

        await UploadAvatarPath();

        _isLoading = false;
        StateHasChanged();
    }

    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_curator != null)
        {
            if (_curator.User != null)
            {
                _curator.Name = _curator.User.Name;
                _curator.User.Role = Roles.Curator;
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

    private void OnSelectedItemChangedHandler(Division value)
    {
        SelectedDivision = value;
        StateHasChanged();
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