using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;

namespace MentorsDiary.Application.Entities.Files.Domain;

public class ServiceFile : BaseUserCU, IHaveId, IHaveName
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? FilePath { get; set; }

    public string Description => $"{Id}";
}