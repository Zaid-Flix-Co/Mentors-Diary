using System.Text.Json.Serialization;
using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Users.Domains;

namespace MentorsDiary.Application.Entities.Curators.Domains;

public class Curator : BaseUserCU, IHaveId, IHaveName, IHaveImage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int UserId { get; set; }

    public string? ImagePath { get; set; }

    public virtual User? User { get; set; }

    public string Description => $"{Id}";
}