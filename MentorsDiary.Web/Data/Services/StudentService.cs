using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class StudentService(IHttpClientFactory clientFactory) : BaseService<Student>(clientFactory);