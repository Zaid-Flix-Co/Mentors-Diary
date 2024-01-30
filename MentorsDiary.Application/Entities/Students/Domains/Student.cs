using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Groups.Domains;

namespace MentorsDiary.Application.Entities.Students.Domains;

public class Student : BaseUserCU, IHaveId, IHaveName, IHaveImage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Address { get; set; }

    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public string? ImagePath { get; set; }

    public string Description => $"{Id}";
}