using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class CuratorService(IHttpClientFactory clientFactory) : BaseService<Curator>(clientFactory);