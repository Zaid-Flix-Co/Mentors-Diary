using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class DivisionService(IHttpClientFactory clientFactory) : BaseService<Division>(clientFactory);