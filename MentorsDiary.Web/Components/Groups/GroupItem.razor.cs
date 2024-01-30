using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MentorsDiary.Web.Components.Groups;

public partial class GroupItem
{
    #region PARAMETERS

    [Parameter]
    public int GroupId { get; set; }

    #endregion

    #region INJECTIONS

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private GroupService GroupService { get; set; } = null!;

    [Inject]
    private CuratorService CuratorService { get; set; } = null!;

    [Inject]
    private DivisionService DivisionService { get; set; } = null!;

    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    private Group? _group = new();

    private List<Division>? Divisions { get; set; } = new();

    private List<Curator>? Curators { get; set; } = new();

    private static string NavigateToUri => "group";

    private bool _isLoading;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await GetListAsync();
    }

    private async Task GetListAsync()
    {
        _isLoading = true;
        StateHasChanged();

        switch (CurrentUser.Role)
        {
            case Roles.Administrator:
                Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();
                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Curator>()).ToList();
                break;
            case Roles.DeputyDirector:
                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Curator>())
                    .Where(c => c.User?.DivisionId == CurrentUser.DivisionId).ToList();
                break;
        }

        _group = await GroupService.GetIdAsync(GroupId);

        _isLoading = false;
        StateHasChanged();
    }

    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_group != null)
        {
            _group.Division = null;
            _group.Curator = null;

            if (CurrentUser.Role == Roles.DeputyDirector)
                _group.DivisionId = CurrentUser.DivisionId;
            
            var response = await GroupService.UpdateAsync(_group);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Группа {_group.Name} успешно добавлена.");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        _isLoading = false;

        NavigationManager.NavigateTo($"/{NavigateToUri}");
    }
}