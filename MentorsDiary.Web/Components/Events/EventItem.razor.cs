using AntDesign;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MentorsDiary.Web.Components.Events;

public partial class EventItem
{
    #region PARAMETERS

    [Parameter]
    public int EventId { get; set; }

    #endregion

    #region INJECTIONS

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private EventService EventService { get; set; } = null!;

    [Inject]
    private MessageService MessageService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    private Event? _event = new();

    private static string NavigateToUri => "event";

    private bool _isLoading;

    private Event? Clone { get; set; } = new();

    private string? _avatar;

    private string? _newAvatar;

    private IBrowserFile? _resizedImage;

    private bool _isLoadingImage;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await GetItemAsync();
    }

    private async Task UploadAvatarPath()
    {
        if (_event!.ImagePath != null)
        {
            var result = await EventService.GetAvatarAsync(_event!.ImagePath);

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

        _event = await EventService.GetIdAsync(EventId);
        
        await UploadAvatarPath();

        _isLoading = false;
        StateHasChanged();
    }

    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_event != null)
        {
            if(_isLoadingImage)
                await UploadAvatar();

            var response = await EventService.UpdateAsync(_event);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Событие {_event.Name} успешно добавлено.");
            else
                await MessageService.Error(response.ReasonPhrase);
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

        var response = await EventService?.UploadAvatarAsync(content)!;

        if (response.IsSuccessStatusCode)
        {
            _event!.ImagePath = fileName;
            Clone!.ImagePath = fileName;

            await MessageService.Success("Upload completed successfully.");
            var result = await EventService.GetAvatarAsync(_event!.ImagePath);
            _avatar = result.RequestMessage?.RequestUri?.ToString();
        }
        else
            await MessageService.Error("Upload failed.");
        _isLoadingImage = false;
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