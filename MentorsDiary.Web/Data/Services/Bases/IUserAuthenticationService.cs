using MentorsDiary.Application.Entities.Users.Domains;

namespace MentorsDiary.Web.Data.Services.Bases;

public interface IUserAuthenticationService
{
    public User? AuthorizedUser { get; set; }
}