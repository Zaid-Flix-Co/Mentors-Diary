using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Application.Entities.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class UserController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Users.Domains.User, MentorsDiary.Application.Entities.Users.Interfaces.IUserRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Users.Domains.User, MentorsDiary.Application.Entities.Users.Interfaces.IUserRepository}" />
public class UserController : BaseController<User, IUserRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public UserController(IUserRepository repository, IWebHostEnvironment env) : base(repository, env)
    {
    }
}