using AntDesign;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.DeputyDirector;

/// <summary>
/// Class DeputyDirectorData.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class DeputyDirectorData
{
    /// <summary>
    /// Gets or sets the deputy director.
    /// </summary>
    /// <value>The deputy director.</value>
    [Parameter]
    public User DeputyDirector { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user service.
    /// </summary>
    /// <value>The user service.</value>
    [Inject]
    private UserService UserService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private MessageService MessageService { get; set; } = null!;

    /// <summary>
    /// The avatar
    /// </summary>
    private string _avatar = null!;

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        if (DeputyDirector.ImagePath != null)
        {
            var result = await UserService.GetAvatarAsync(DeputyDirector.ImagePath)!;
            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString()!;
            else
                await MessageService.Error("Ошибка фотографии.");
        }
    }
}