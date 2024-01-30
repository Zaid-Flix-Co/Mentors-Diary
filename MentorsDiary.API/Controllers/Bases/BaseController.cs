using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Bases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MentorsDiary.API.Controllers.Bases;

/// <summary>
/// Class BaseController.
/// Implements the <see cref="ControllerBase" />
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
/// <typeparam name="TRepository">The type of the t repository.</typeparam>
/// <seealso cref="ControllerBase" />
[Route("api/[controller]")]
public class BaseController<TEntity, TRepository> : ControllerBase
    where TEntity : class, IHaveId, IHaveName
    where TRepository : class, IBaseRepository<TEntity>
{
    /// <summary>
    /// The repository
    /// </summary>
    private readonly TRepository _repository;

    /// <summary>
    /// The env
    /// </summary>
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// The resources
    /// </summary>
    private const string Resources = "Resources";

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseController{TEntity, TRepository}" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public BaseController(TRepository repository, IWebHostEnvironment env)
    {
        _repository = repository;
        _env = env;
    }

    /// <summary>
    /// Gets all.
    /// </summary>
    /// <returns>System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;.</returns>
    [HttpGet]
    public async Task<IEnumerable<TEntity>?> GetAll()
    {
        var entities = await _repository.GetAll();
        return entities ?? null;
    }

    /// <summary>
    /// Gets all by filter.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;.</returns>
    [HttpPost("Filter/{query}")]
    public async Task<IEnumerable<TEntity>?> GetAllByFilter(string query)
    {
        var entities = await _repository.GetAllByFilter(query);
        return entities ?? null;
    }

    /// <summary>
    /// Gets all by filter.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;.</returns>
    [HttpPost("Filter")]
    public async Task<IEnumerable<TEntity>?> GetAllByFilter([FromBody] FilterParams query)
    {
        if (query.FilterValue == "-1")
            return await _repository.GetAll();

        var entities = await _repository.GetAllByFilter(query);
        return entities ?? null;
    }

    /// <summary>
    /// Gets the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>System.Nullable&lt;TEntity&gt;.</returns>
    [HttpPost("{id:int}")]
    public async Task<ActionResult<TEntity>?> Get(int id)
    {
        var entity = await _repository.GetById(id);
        return Ok(entity ?? null);
    }

    /// <summary>
    /// Creates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>System.Nullable&lt;TEntity&gt;.</returns>
    [HttpPost("Create")]
    public async Task<TEntity?> Create([FromBody] TEntity entity)
    {
        await _repository.Add(entity);
        return entity;
    }

    /// <summary>
    /// Puts the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>System.Nullable&lt;TEntity&gt;.</returns>
    [HttpPost("Update")]
    public async Task<TEntity?> Put([FromBody] TEntity entity)
    {
        await _repository.Update(entity);
        return entity;
    }

    /// <summary>
    /// Deletes the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>System.Nullable&lt;TEntity&gt;.</returns>
    [HttpDelete("{id}")]
    public async Task<TEntity?> Delete(int id)
    {
        var entity = (_repository.Find(x => x.Id == id).Result ?? Array.Empty<TEntity>()).FirstOrDefault();

        if (entity != null)
        {
            await _repository.Remove(entity);
            return entity;
        }

        return null;
    }

    /// <summary>
    /// Saves the image.
    /// </summary>
    /// <param name="files">The files.</param>
    /// <returns>IFormFile.</returns>
    [HttpPost("UploadAvatar")]
    public async Task<IFormFile> UploadAvatar([FromForm] List<IFormFile> files)
    {
        if (!Directory.Exists(Path.Combine(_env.ContentRootPath, Resources)))
            Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, Resources));

        var path = Path.Combine(_env.ContentRootPath, Resources, files[0].FileName);

        await using FileStream fs = new(path, FileMode.Create);
        await files[0].CopyToAsync(fs);

        return files[0];
    }

    /// <summary>
    /// Retrieves image.
    /// </summary>
    /// <param name="avatarPath">Path to the image.</param>
    /// <returns>File.</returns>
    [HttpGet("GetAvatar/{avatarPath}")]
    public async Task<IActionResult> GetAvatar(string avatarPath)
    {
        if (!Directory.Exists(Path.Combine(_env.ContentRootPath, Resources)))
            Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, Resources));

        if (System.IO.File.Exists(Path.Combine(_env.ContentRootPath, Resources, avatarPath)))
        {
            var fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(_env.ContentRootPath, Resources, avatarPath));
            return File(fileBytes, "image/png");
        }

        return NoContent();
    }
}