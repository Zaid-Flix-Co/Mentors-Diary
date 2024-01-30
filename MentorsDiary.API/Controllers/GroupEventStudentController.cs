using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class GroupEventStudentController.
/// Implements the <see cref="BaseController{GroupEventStudent, IGroupEventStudentRepository}" />
/// </summary>
/// <seealso cref="BaseController{GroupEventStudent, IGroupEventStudentRepository}" />
public class GroupEventStudentController : BaseController<GroupEventStudent, IGroupEventStudentRepository>
{
    /// <summary>
    /// The repository
    /// </summary>
    private readonly IGroupEventStudentRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventStudentController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public GroupEventStudentController(IGroupEventStudentRepository repository, IWebHostEnvironment env) : base(repository, env)
    {
        _repository = repository;
    }

    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="groupEventStudents">The group event students.</param>
    /// <returns>List&lt;GroupEventStudent&gt;.</returns>
    [HttpPost("AddStudentsInGroupEvent")]
    public async Task<List<GroupEventStudent>> AddStudentsInGroupEvent([FromBody] List<GroupEventStudent> groupEventStudents)
    {
        await _repository.AddStudentsInGroupEvent(groupEventStudents);
        return groupEventStudents;
    }
}