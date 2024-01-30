using HttpService.Model.User.Domain;
using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Divisions.Domains;
using System.ComponentModel.DataAnnotations;

namespace MentorsDiary.Application.Entities.Users.Domains;

public class User : BaseUserCU, IHaveId, IHaveName, IHaveImage, IBaseUser
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Имя")] 
    public string? Name { get; set; }

    [Display(Name = "Пароль")]
    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? ImagePath { get; set; }

    public Roles? Role { get; set; }

    public int? DivisionId { get; set; }

    public virtual Division? Division { get; set; }

    public string Description => $"{Id}";
}