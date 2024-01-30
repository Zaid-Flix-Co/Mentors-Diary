using System.ComponentModel.DataAnnotations.Schema;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.ParentStudents.Domains;

public class ParentStudent : IHaveId, IHaveName
{
    public int Id { get; set; }

    [NotMapped]
    public string? Name { get; set; }

    public int StudentId { get; set; }

    public virtual Student? Student { get; set; }

    public int ParentId { get; set; }

    public virtual Parent? Parent { get; set; }

    public string Description => $"{Id}";
}