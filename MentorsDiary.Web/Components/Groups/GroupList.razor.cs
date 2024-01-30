using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Components.Groups;

public partial class GroupList
{
    #region INJECTIONS

    [Inject]
    private GroupService GroupService { get; set; } = null!;

    [Inject]
    private CuratorService CuratorService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    private bool _isLoading;

    private List<Group>? Groups { get; set; } = new();

    private static string NavigateToUri => "group";

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
                Groups = (await GroupService.GetAllAsync() ?? Array.Empty<Group>()).ToList();
                break;
            case Roles.DeputyDirector:
                {
                    Groups = (await GroupService.GetAllByFilterAsync(
                        new FilterParams
                        {
                            ColumnName = "DivisionId",
                            FilterOption = FilterOptions.Contains,
                            FilterValue = CurrentUser.DivisionId.ToString()!
                        }) ?? Array.Empty<Group>()).ToList();
                    break;
                }
            case Roles.Curator:
                {
                    var userId = (await CuratorService.GetAllByFilterAsync(
                        new FilterParams
                        {
                            ColumnName = "UserId",
                            FilterOption = FilterOptions.Contains,
                            FilterValue = CurrentUser.Id.ToString()!
                        }) ?? Array.Empty<Curator>()).FirstOrDefault()!.Id;

                    Groups = (await GroupService.GetAllByFilterAsync(
                    new FilterParams
                    {
                        ColumnName = "CuratorId",
                        FilterOption = FilterOptions.Contains,
                        FilterValue = userId.ToString()!
                    }) ?? Array.Empty<Group>()).ToList();
                    break;
                }
        }

        _isLoading = false;
        StateHasChanged();
    }

    public async Task CreateGroupAsync()
    {
        _isLoading = true;
        StateHasChanged();

        var response = new HttpResponseMessage();

        switch (CurrentUser.Role)
        {
            case Roles.Administrator:
                response = await GroupService.CreateAsync(new Group
                {
                    DateCreated = DateTime.Now,
                    UserCreated = CurrentUser.Name
                });
                break;
            case Roles.DeputyDirector:
                response = await GroupService.CreateAsync(new Group
                {
                    DivisionId = CurrentUser.DivisionId,
                    DateCreated = DateTime.Now,
                    UserCreated = CurrentUser.Name
                });
                break;
        }

        if (response.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"{NavigateToUri}/{JsonConvert.DeserializeObject<Group>(await response.Content.ReadAsStringAsync())!.Id}");
        else
            await MessageService.Error(response.ReasonPhrase);

        _isLoading = false;
        StateHasChanged();
    }

    private async Task UpdateList(Division? division)
    {
        if (division != null)
        {
            _isLoading = true;
            StateHasChanged();

            if (division.Name != null)
            {
                Groups = (await GroupService.GetAllByFilterAsync(
                    new FilterParams
                    {
                        ColumnName = "DivisionId",
                        FilterOption = FilterOptions.Contains,
                        FilterValue = division.Id.ToString()
                    }) ?? Array.Empty<Group>()).ToList();
            }

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    private async Task SearchList(string query)
    {
        if (query != null)
        {
            _isLoading = true;
            StateHasChanged();

            Groups = (await GroupService.GetAllByFilterAsync(query) ?? Array.Empty<Group>()).ToList();

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    private async Task RemoveAsync(Group group)
    {
        _isLoading = true;
        StateHasChanged();

        var response = await GroupService.DeleteAsync(group.Id);

        if (response.IsSuccessStatusCode)
            await MessageService.Success($"Группа {group.Name} успешно удалена.");
        else
            await MessageService.Error(response.ReasonPhrase);

        await GetListAsync();

        _isLoading = false;
        StateHasChanged();
    }

    private void UpdateAsync(IHaveId group)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{group.Id}");
    }

    private void ShowGroupPageAsync(IHaveId group)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}-page/{group.Id}");
    }
}