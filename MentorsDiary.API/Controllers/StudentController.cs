using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Application.Entities.Students.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class StudentController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Students.Domains.Student, MentorsDiary.Application.Entities.Students.Interfaces.IStudentRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.Students.Domains.Student, MentorsDiary.Application.Entities.Students.Interfaces.IStudentRepository}" />
public class StudentController : BaseController<Student, IStudentRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public StudentController(IStudentRepository repository, IWebHostEnvironment env) : base(repository, env)
    {

    }
}