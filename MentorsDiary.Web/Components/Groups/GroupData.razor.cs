using AntDesign;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Groups;

public partial class GroupData
{
    [Parameter]
    public Group Group { get; set; } = null!;

    [Inject]
    private UserService UserService { get; set; } = null!;

    private string _avatar = null!;

    protected override async Task OnInitializedAsync()
    {
        if (Group.ImagePath != null)
        {
            var result = await UserService.GetAvatarAsync(Group.ImagePath);

            if (result.IsSuccessStatusCode)
                _avatar = result.RequestMessage?.RequestUri?.ToString()!;
            else
                Console.WriteLine("Ошибка фотографии.");
        }
    }
}