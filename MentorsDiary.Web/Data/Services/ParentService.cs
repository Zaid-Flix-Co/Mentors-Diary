using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class ParentService(IHttpClientFactory clientFactory) : BaseService<Parent>(clientFactory);