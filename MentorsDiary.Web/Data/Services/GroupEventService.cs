using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class GroupEventService(IHttpClientFactory clientFactory) : BaseService<GroupEvent>(clientFactory);