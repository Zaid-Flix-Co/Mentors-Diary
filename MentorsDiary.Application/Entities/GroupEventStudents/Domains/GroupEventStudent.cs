using System.ComponentModel.DataAnnotations.Schema;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.GroupEventStudents.Domains;

public class GroupEventStudent : IHaveId, IHaveName
{
    public int Id { get; set; }

    public int GroupEventId { get; set; }

    public virtual GroupEvent? GroupEvent { get; set; }

    public int StudentId { get; set; }

    public virtual Student? Student { get; set; }

    [NotMapped]
    public string? Name { get; set; }

    public string Description => $"{Id}";
}