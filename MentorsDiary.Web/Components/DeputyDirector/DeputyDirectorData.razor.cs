using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.DeputyDirector;

public partial class DeputyDirectorData
{
    [Parameter]
    public User DeputyDirector { get; set; } = null!;

    [Inject]
    private UserService UserService { get; set; } = null!;

    private string _avatar = null!;

    protected override async Task OnInitializedAsync()
    {
        if (DeputyDirector.ImagePath != null)
        {
            var result = await UserService.GetAvatarAsync(DeputyDirector.ImagePath);

            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString()!;
            else
                Console.WriteLine("Ошибка фотографии.");
        }
    }
}