using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;

namespace MentorsDiary.Application.Entities.Groups.Domains;

public class Group : BaseUserCU, IHaveId, IHaveName, IHaveImage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [NotMapped]
    [JsonIgnore]
    public string? LongName => $"{Name}[Текущий куратор: {Curator?.Name}]";

    public int? DivisionId { get; set; }

    public virtual Division? Division { get; set; }

    public string? ImagePath { get; set; }

    public int? CuratorId { get; set; }

    public virtual Curator? Curator { get; set; }

    public string Description => $"{Id}";
}