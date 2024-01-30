using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Microsoft.JSInterop;

namespace MentorsDiary.Web.Components.Curators;

/// <summary>
/// Class CuratorList.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class CuratorList
{
    /// <summary>
    /// Gets or sets the curator.
    /// </summary>
    /// <value>The curator.</value>
    [Parameter]
    public User? Curator { get; set; }

    #region INJECTIONS
    
    /// <summary>
    /// Gets or sets the user service.
    /// </summary>
    /// <value>The user service.</value>
    [Inject]
    private UserService UserService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the curator service.
    /// </summary>
    /// <value>The curator service.</value>
    [Inject]
    private CuratorService CuratorService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private MessageService MessageService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the authentication service.
    /// </summary>
    /// <value>The authentication service.</value>
    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <value>The current user.</value>
    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// The users
    /// </summary>
    /// <value>The users.</value>
    private List<User>? Users { get; set; }

    /// <summary>
    /// Gets or sets the curators.
    /// </summary>
    /// <value>The curators.</value>
    private List<Curator>? Curators { get; set; }

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "curator";

    [Inject]
    private IJSRuntime Js { get; set; } = null!;

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        await GetListAsync();
    }

    /// <summary>
    /// Get list as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task GetListAsync()
    {
        _isLoading = true;
        StateHasChanged();

        switch (CurrentUser.Role)
        {
            case Roles.Administrator:
                Users = (await UserService.GetAllAsync() ?? Array.Empty<User>()).Where(u => u.Role == Roles.Curator).ToList();

                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Curator>()).Where(c => Users.Any(u => u.Id == c.UserId)).ToList();
                break;
            case Roles.DeputyDirector:
                Users = (await UserService.GetAllAsync() ?? Array.Empty<User>()).Where(u => u.Role == Roles.Curator && u.DivisionId == CurrentUser.DivisionId)
                    .ToList();

                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Curator>()).Where(c => Users.Any(u => u.Id == c.UserId)).ToList();
                break;
        }

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Create curator as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CreateCuratorAsync()
    {
        _isLoading = true;
        StateHasChanged();

        var responseUserCreate = new HttpResponseMessage();

        switch (CurrentUser.Role)
        {
            case Roles.Administrator:
                responseUserCreate = await UserService.CreateAsync(new User
                {
                    DateCreated = DateTime.Now,
                    UserCreated = CurrentUser.Name
                });
                break;
            case Roles.DeputyDirector:
                responseUserCreate = await UserService.CreateAsync(new User
                {
                    DivisionId = CurrentUser.DivisionId,
                    DateCreated = DateTime.Now,
                    UserCreated = CurrentUser.Name
                });
                break;
        }

        var deserializeObject = JsonConvert.DeserializeObject<User>(await responseUserCreate.Content.ReadAsStringAsync());

        if (deserializeObject != null)
        {
            var responseCuratorCreate =
                await CuratorService.CreateAsync(new Curator
                {
                    UserId = deserializeObject.Id
                });

            if (responseUserCreate.IsSuccessStatusCode && responseCuratorCreate.IsSuccessStatusCode)
                NavigationManager.NavigateTo(
                    $"{NavigateToUri}/{JsonConvert.DeserializeObject<Curator>(await responseCuratorCreate.Content.ReadAsStringAsync())!.Id}");
            else
                await MessageService.Error($"{responseUserCreate.ReasonPhrase}\n{responseCuratorCreate.ReasonPhrase}");
        }

        _isLoading = false;
    }

    /// <summary>
    /// Updates the list.
    /// </summary>
    /// <param name="division">The division.</param>
    private async Task UpdateList(Division? division)
    {
        if (division != null)
        {
            await GetListAsync();

            _isLoading = true;
            StateHasChanged();

            if (division.Name != null)
            {
                Users = (await UserService.GetAllByFilterAsync(
                    new FilterParams
                    {
                        ColumnName = "DivisionId",
                        FilterOption = FilterOptions.Contains,
                        FilterValue = division.Id.ToString()
                    }) ?? Array.Empty<User>()).ToList();
            }

            for (var index = Curators!.Count - 1; index >= 0; index--)
            {
                var flag = true;
                for (var i = 0; i < Users!.Count; i++)
                {
                    if (Curators[index].UserId != Users[i].Id) continue;
                    flag = false;
                    break;
                }
                if (flag)
                {
                    Curators.RemoveAt(index);
                }
            }

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Searches the list.
    /// </summary>
    /// <param name="query">The query.</param>
    private async Task SearchList(string? query)
    {
        if (query != string.Empty)
        {
            _isLoading = true;
            StateHasChanged();

            Curators = (await CuratorService.GetAllByFilterAsync(query!) ??
                        Array.Empty<Curator>()).ToList();

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Remove as an asynchronous operation.
    /// </summary>
    /// <param name="curator">The curator.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveAsync(Curator curator)
    {
        var responseDeleteUser = await UserService.DeleteAsync(curator.UserId);
        var responseDeleteCurator = await CuratorService.DeleteAsync(curator.Id);

        if (responseDeleteUser.IsSuccessStatusCode && responseDeleteCurator.IsSuccessStatusCode)
            await MessageService.Success($"Пользователь {curator.Name} успешно удален.");
        else
            await MessageService.Error($"{responseDeleteUser.ReasonPhrase}\n{responseDeleteCurator.ReasonPhrase}");

        await GetListAsync();

        NavigationManager.NavigateTo("/curator", true);
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="curator">The curator.</param>
    private void UpdateAsync(IHaveId curator)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{curator.Id}");
    }

	public async Task DownloadExcelFileCurators()
	{
		var excelBytes = ExportCurators();
		await Js.InvokeVoidAsync("saveAsFile", $"Export_Curators_{DateTime.Now:d}.xlsx", Convert.ToBase64String(excelBytes));
	}

    private byte[] ExportCurators()
    {
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            var workSheetDataCurators = package.Workbook.Worksheets.Add("Кураторы");

            using (var cells = workSheetDataCurators.Cells["A1:E1"])
            {
                cells.Merge = true;
                cells.Value = "Кураторы академических групп";
            }

            workSheetDataCurators.Cells[2, 1].Value = "№ п/п";
            workSheetDataCurators.Cells[2, 2].Value = "ФИО";
            workSheetDataCurators.Cells[2, 3].Value = "Подразделение";
            workSheetDataCurators.Cells[2, 4].Value = "Телефон";
            workSheetDataCurators.Cells[2, 5].Value = "Email";

            workSheetDataCurators.Cells["A1:E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheetDataCurators.Cells["A1:H2"].Style.Font.Bold = true;

            workSheetDataCurators.Cells["A1:A1000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            var startRowId = 3;
            var numberOfTheCurator = 0;
            foreach (var curator in Curators!)
            {
                workSheetDataCurators.Cells[startRowId, 2].Value = curator.Name;
                workSheetDataCurators.Cells[startRowId, 3].Value = curator.User.Division.Name;
                workSheetDataCurators.Cells[startRowId, 4].Value = curator.User.Phone;
                workSheetDataCurators.Cells[startRowId, 5].Value = curator.User.Email;

                if (workSheetDataCurators.Cells[startRowId, 2].Value != null)
                {
                    numberOfTheCurator++;
                    workSheetDataCurators.Cells[startRowId, 1].Value = numberOfTheCurator;
                }

                startRowId++;
            }

            workSheetDataCurators.Cells["A1:H100"].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}