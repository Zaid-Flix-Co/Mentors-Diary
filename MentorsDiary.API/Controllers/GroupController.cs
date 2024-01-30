using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Groups.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class GroupController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Groups.Domains.Group, MentorsDiary.Application.Entities.Groups.Interfaces.IGroupRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Groups.Domains.Group, MentorsDiary.Application.Entities.Groups.Interfaces.IGroupRepository}" />
public class GroupController : BaseController<Group, IGroupRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public GroupController(IGroupRepository repository, IWebHostEnvironment env) : base(repository, env)
    {

    }
}