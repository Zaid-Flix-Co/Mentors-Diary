using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class ParentStudentController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.ParentStudents.Domains.ParentStudent, MentorsDiary.Application.Entities.ParentStudents.Interfaces.IParentStudentRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.ParentStudents.Domains.ParentStudent, MentorsDiary.Application.Entities.ParentStudents.Interfaces.IParentStudentRepository}" />
public class ParentStudentController : BaseController<ParentStudent, IParentStudentRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentStudentController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public ParentStudentController(IParentStudentRepository repository, IWebHostEnvironment env) : base(repository, env)
    {

    }
}