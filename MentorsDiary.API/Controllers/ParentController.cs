using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.Parents.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class ParentController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Parents.Domains.Parent, MentorsDiary.Application.Entities.Parents.Interfaces.IParentRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Parents.Domains.Parent, MentorsDiary.Application.Entities.Parents.Interfaces.IParentRepository}" />
public class ParentController : BaseController<Parent, IParentRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public ParentController(IParentRepository repository, IWebHostEnvironment env) : base(repository, env)
    {

    }
}