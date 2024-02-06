using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class EventService(IHttpClientFactory clientFactory) : BaseService<Event>(clientFactory);