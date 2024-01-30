using AntDesign;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Events;

/// <summary>
/// Class EventData.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class EventData
{
    /// <summary>
    /// Gets or sets the event.
    /// </summary>
    /// <value>The event.</value>
    [Parameter]
    public Event Event { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user service.
    /// </summary>
    /// <value>The user service.</value>
    [Inject]
    private EventService EventService { get; set; } = null!;

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
        if (Event.ImagePath != null)
        {
            var result = await EventService.GetAvatarAsync(Event.ImagePath)!;
            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString()!;
            else
                await MessageService.Error("Ошибка фотографии.");
        }
    }
}