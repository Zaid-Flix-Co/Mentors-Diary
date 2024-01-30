using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class GroupEventController.
/// Implements the <see cref="BaseController{GroupEvent, IGroupEventRepository}" />
/// </summary>
/// <seealso cref="BaseController{GroupEvent, IGroupEventRepository}" />
public class GroupEventController : BaseController<GroupEvent, IGroupEventRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public GroupEventController(IGroupEventRepository repository, IWebHostEnvironment env) : base(repository, env)
    {
    }
}