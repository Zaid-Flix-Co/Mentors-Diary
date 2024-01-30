using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorsDiary.Application.Entities.GroupEvents.Domains;

public class GroupEvent : BaseUserCU, IHaveId, IHaveName
{
    public int Id { get; set; }

    [NotMapped]
    public string? Name { get; set; }

    public int? CountParticipants { get; set; }

    public int? GroupId { get; set; }

    public virtual Group? Group { get; set; }

    public int? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public string Description => $"{Id}";
}
