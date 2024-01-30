using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Events;

public partial class EventData
{
    [Parameter]
    public Event Event { get; set; } = null!;

    [Inject]
    private EventService EventService { get; set; } = null!;

    private string _avatar = null!;

    protected override async Task OnInitializedAsync()
    {
        if (Event.ImagePath != null)
        {
            var result = await EventService.GetAvatarAsync(Event.ImagePath)!;

            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString()!;
            else
                Console.WriteLine("Ошибка фотографии");
        }
    }
}