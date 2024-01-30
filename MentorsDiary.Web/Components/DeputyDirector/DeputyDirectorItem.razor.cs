using AntDesign;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MentorsDiary.Web.Components.DeputyDirector;

public partial class DeputyDirectorItem
{
    #region PARAMETERS

    [Parameter]
    public int DeputyDirectorId { get; set; }

    #endregion

    #region INJECTIONS

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private UserService UserService { get; set; } = null!;

    [Inject]
    private DivisionService DivisionService { get; set; } = null!;

    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    private User? _deputyDirector = new();

    private List<Division>? Divisions { get; set; } = new();

    private Division? SelectedDivision { get; set; } = new();

    private User? Clone { get; set; } = new();

    private static string NavigateToUri => "deputydirector";

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
        if (_deputyDirector!.ImagePath != null)
        {
            var result = await UserService?.GetAvatarAsync(_deputyDirector!.ImagePath)!;

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
        _deputyDirector = await UserService.GetIdAsync(DeputyDirectorId);

        await UploadAvatarPath();

        _isLoading = false;
        StateHasChanged();
    }

    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_deputyDirector != null)
        {
            _deputyDirector.Role = Roles.DeputyDirector;
            _deputyDirector.Division = null;

            if (_isLoadingImage)
                await UploadAvatar();

            var response = await UserService.UpdateAsync(_deputyDirector);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Пользователь {_deputyDirector.Name} успешно добавлен.");
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

        var response = await UserService.UploadAvatarAsync(content)!;

        if (response.IsSuccessStatusCode)
        {
            _deputyDirector!.ImagePath = fileName;
            Clone!.ImagePath = fileName;

            var result = await UserService.GetAvatarAsync(_deputyDirector!.ImagePath);
            _avatar = result.RequestMessage?.RequestUri?.ToString();
        }
        else
            await MessageService.Error("Upload failed.");

        _isLoadingImage = false;
    }

    private void OnSelectedItemChangedHandler(Division division)
    {
        SelectedDivision = division;
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