using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class ParentStudentService(IHttpClientFactory clientFactory) : BaseService<ParentStudent>(clientFactory);