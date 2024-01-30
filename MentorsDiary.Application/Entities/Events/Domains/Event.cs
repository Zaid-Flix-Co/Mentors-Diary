using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;

namespace MentorsDiary.Application.Entities.Events.Domains;

public class Event : BaseUserCU, IHaveId, IHaveName, IHaveImage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Comment { get; set; }

    public DateTime? DateEvent { get; set; }

    public string? ImagePath { get; set; }

    public string Description => $"{Id}";
}