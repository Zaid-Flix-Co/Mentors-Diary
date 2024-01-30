using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;

namespace MentorsDiary.Application.Entities.Divisions.Domains;

public class Division : BaseUserCU, IHaveId, IHaveName
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Description => $"{Id}";
}