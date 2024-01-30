using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.Events.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class EventController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Events.Domains.Event, MentorsDiary.Application.Entities.Events.Interfaces.IEventRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Events.Domains.Event, MentorsDiary.Application.Entities.Events.Interfaces.IEventRepository}" />
public class EventController : BaseController<Event, IEventRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public EventController(IEventRepository repository, IWebHostEnvironment env) : base(repository, env)
    {

    }
}
