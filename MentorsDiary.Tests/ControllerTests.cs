using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Curators.Interfaces;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Divisions.Interfaces;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.Events.Interfaces;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Interfaces;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Groups.Interfaces;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.Parents.Interfaces;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Interfaces;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Application.Entities.Students.Interfaces;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Application.Entities.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Xml;
using Assert = NUnit.Framework.Assert;

namespace MentorsDiary.Tests;

/// <summary>
/// Defines test class ControllerTests.
/// </summary>
[TestFixture]
public class ControllerTests
{
    /// <summary>
    /// The curator controller
    /// </summary>
    private BaseController<Curator, ICuratorRepository> _curatorController;

    /// <summary>
    /// The division controller
    /// </summary>
    private BaseController<Division, IDivisionRepository> _divisionController;

    /// <summary>
    /// The event controller
    /// </summary>
    private BaseController<Event, IEventRepository> _eventController;

    /// <summary>
    /// The user controller
    /// </summary>
    private BaseController<User, IUserRepository> _userController;

    /// <summary>
    /// The group controller
    /// </summary>
    private BaseController<Group, IGroupRepository> _groupController;

    /// <summary>
    /// The group event controller
    /// </summary>
    private BaseController<GroupEvent, IGroupEventRepository> _groupEventController;

    /// <summary>
    /// The parent controller
    /// </summary>
    private BaseController<Parent, IParentRepository> _parentController;

    /// <summary>
    /// The parent student controller
    /// </summary>
    private BaseController<ParentStudent, IParentStudentRepository> _parentStudentController;

    /// <summary>
    /// The student controller
    /// </summary>
    private BaseController<Student, IStudentRepository> _studentController;

    /// <summary>
    /// The curator repository mock
    /// </summary>
    private Mock<ICuratorRepository> _curatorRepositoryMock;

    /// <summary>
    /// The division repository mock
    /// </summary>
    private Mock<IDivisionRepository> _divisionRepositoryMock;

    /// <summary>
    /// The event repository mock
    /// </summary>
    private Mock<IEventRepository> _eventRepositoryMock;

    /// <summary>
    /// The user repository mock
    /// </summary>
    private Mock<IUserRepository> _userRepositoryMock;

    /// <summary>
    /// The group repository mock
    /// </summary>
    private Mock<IGroupRepository> _groupRepositoryMock;

    /// <summary>
    /// The group event repository mock
    /// </summary>
    private Mock<IGroupEventRepository> _groupEventRepositoryMock;

    /// <summary>
    /// The parent repository mock
    /// </summary>
    private Mock<IParentRepository> _parentRepositoryMock;

    /// <summary>
    /// The parent student repository mock
    /// </summary>
    private Mock<IParentStudentRepository> _parentStudentRepositoryMock;

    /// <summary>
    /// The student repository mock
    /// </summary>
    private Mock<IStudentRepository> _studentRepositoryMock;

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _curatorRepositoryMock = new Mock<ICuratorRepository>();
        _divisionRepositoryMock = new Mock<IDivisionRepository>();
        _eventRepositoryMock = new Mock<IEventRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _groupRepositoryMock = new Mock<IGroupRepository>();
        _groupEventRepositoryMock = new Mock<IGroupEventRepository>();
        _parentRepositoryMock = new Mock<IParentRepository>();
        _parentStudentRepositoryMock = new Mock<IParentStudentRepository>();
        _studentRepositoryMock = new Mock<IStudentRepository>();

        _curatorController = new BaseController<Curator, ICuratorRepository>(_curatorRepositoryMock.Object, null!);
        _divisionController = new BaseController<Division, IDivisionRepository>(_divisionRepositoryMock.Object, null!);
        _eventController = new BaseController<Event, IEventRepository>(_eventRepositoryMock.Object, null!);
        _userController = new BaseController<User, IUserRepository>(_userRepositoryMock.Object, null!);
        _groupController = new BaseController<Group, IGroupRepository>(_groupRepositoryMock.Object, null!);
        _groupEventController = new BaseController<GroupEvent, IGroupEventRepository>(_groupEventRepositoryMock.Object, null!);
        _parentController = new BaseController<Parent, IParentRepository>(_parentRepositoryMock.Object, null!);
        _parentStudentController = new BaseController<ParentStudent, IParentStudentRepository>(_parentStudentRepositoryMock.Object, null!);
        _studentController = new BaseController<Student, IStudentRepository>(_studentRepositoryMock.Object, null!);
    }

    /// <summary>
    /// Defines the test method GetAll_Users_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_Users_ReturnsEntities()
    {
        // Arrange
        var entities = new List<User> { new(), new() };
        _userRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _userController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method GetAll_Curators_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_Curators_ReturnsEntities()
    {
        // Arrange
        var entities = new List<Curator> { new(), new() };
        _curatorRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _curatorController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method GetAll_Divisions_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_Divisions_ReturnsEntities()
    {
        // Arrange
        var entities = new List<Division> { new(), new() };
        _divisionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _divisionController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method GetAll_Events_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_Events_ReturnsEntities()
    {
        // Arrange
        var entities = new List<Event> { new(), new() };
        _eventRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _eventController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method GetAll_Groups_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_Groups_ReturnsEntities()
    {
        // Arrange
        var entities = new List<Group> { new(), new() };
        _groupRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _groupController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method GetAll_GroupEvents_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_GroupEvents_ReturnsEntities()
    {
        // Arrange
        var entities = new List<GroupEvent> { new(), new() };
        _groupEventRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _groupEventController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method GetAll_Parents_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_Parents_ReturnsEntities()
    {
        // Arrange
        var entities = new List<Parent> { new(), new() };
        _parentRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _parentController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method GetAll_ParentStudents_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_ParentStudents_ReturnsEntities()
    {
        // Arrange
        var entities = new List<ParentStudent> { new(), new() };
        _parentStudentRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _parentStudentController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method GetAll_Students_ReturnsEntities.
    /// </summary>
    [Test]
    public async Task GetAll_Students_ReturnsEntities()
    {
        // Arrange
        var entities = new List<Student> { new(), new() };
        _studentRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(entities);

        // Act
        var result = await _studentController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(entities, result);
    }

    /// <summary>
    /// Defines the test method Get_ReturnsEntity_WhenEntityExists.
    /// </summary>
    [Test]
    public async Task Get_ReturnsEntity_WhenEntityExists()
    {
        // Arrange
        int id = 1;
        var entity = new User() { Id = id, Name = "Entity 1" };

        // Установка ожидаемого поведения Mock-объекта
        _userRepositoryMock.Setup(x => x.GetById(id)).ReturnsAsync(entity);

        // Act
        var result = await _userController.Get(id);

        // Assert
        Assert.True(result.Result is OkObjectResult);

        var okResult = result.Result as OkObjectResult;
        Assert.AreEqual(entity, okResult.Value);
    }

    /// <summary>
    /// Defines the test method Get_ReturnsNull_WhenEntityDoesNotExist.
    /// </summary>
    [Test]
    public async Task Get_ReturnsNull_WhenEntityDoesNotExist()
    {
        // Arrange
        const int id = 2;
        _userRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync((User)null!);

        // Act
        var result = await _userController.Get(id);

        // Assert
        Assert.IsInstanceOf<ActionResult<User>>(result);
        Assert.IsNull(result?.Value);
    }
}