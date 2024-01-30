using AntDesign;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Groups;

/// <summary>
/// Class GroupData.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupData
{
    /// <summary>
    /// Gets or sets the group.
    /// </summary>
    /// <value>The group.</value>
    [Parameter]
    public Group Group { get; set; } = null!;

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
        if (Group.ImagePath != null)
        {
            var result = await UserService.GetAvatarAsync(Group.ImagePath)!;
            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString()!;
            else
                await MessageService.Error("Ошибка фотографии.");
        }
    }
}