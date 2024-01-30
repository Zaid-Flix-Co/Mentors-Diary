using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;

namespace MentorsDiary.Application.Entities.Parents.Domains;

public class Parent : BaseUserCU, IHaveId, IHaveName
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool Sex { get; set; }

    public string? WorkName { get; set; }

    public string? Phone { get; set; }
    
    public string Description => $"{Id}";
}