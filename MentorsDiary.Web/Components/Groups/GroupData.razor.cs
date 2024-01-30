using MentorsDiary.Application.Entities.Groups.Domains;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Groups;

public partial class GroupData
{
    [Parameter]
    public Group Group { get; set; } = null!;
}