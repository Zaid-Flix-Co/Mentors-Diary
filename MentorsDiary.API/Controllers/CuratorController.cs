using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Curators.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class CuratorController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Curators.Domains.Curator, MentorsDiary.Application.Entities.Curators.Interfaces.ICuratorRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Curators.Domains.Curator, MentorsDiary.Application.Entities.Curators.Interfaces.ICuratorRepository}" />
public class CuratorController : BaseController<Curator, ICuratorRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CuratorController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public CuratorController(ICuratorRepository repository, IWebHostEnvironment env) : base(repository, env)
    {

    }
}