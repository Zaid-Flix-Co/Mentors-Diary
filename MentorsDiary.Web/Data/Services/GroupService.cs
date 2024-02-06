using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class GroupService(IHttpClientFactory clientFactory) : BaseService<Group>(clientFactory);