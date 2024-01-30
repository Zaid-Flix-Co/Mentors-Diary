using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Curators;

public partial class CuratorData
{
    [Parameter]
    public Curator Curator { get; set; } = null!;

    [Inject]
    private UserService UserService { get; set; } = null!;

    private string _avatar = null!;

    protected override async Task OnInitializedAsync()
    {
        if (Curator.ImagePath != null)
        {
            var result = await UserService.GetAvatarAsync(Curator.ImagePath);

            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString()!;
            else
                Console.WriteLine("Ошибка фотографии");
        }
    }
}