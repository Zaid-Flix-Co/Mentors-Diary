using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Divisions.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class DivisionController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Divisions.Domains.Division, MentorsDiary.Application.Entities.Divisions.Interfaces.IDivisionRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Divisions.Domains.Division, MentorsDiary.Application.Entities.Divisions.Interfaces.IDivisionRepository}" />
public class DivisionController : BaseController<Division, IDivisionRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DivisionController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public DivisionController(IDivisionRepository repository, IWebHostEnvironment env) : base(repository, env)
    {

    }
}