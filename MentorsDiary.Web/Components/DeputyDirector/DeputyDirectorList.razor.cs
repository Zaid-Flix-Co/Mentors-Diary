using AntDesign;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Microsoft.JSInterop;
using MentorsDiary.Application.Entities.Curators.Domains;

namespace MentorsDiary.Web.Components.DeputyDirector;

/// <summary>
/// Class DeputyDirectorList.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class DeputyDirectorList
{
    #region INJECTIONS

    /// <summary>
    /// Gets or sets the user service.
    /// </summary>
    /// <value>The user service.</value>
    [Inject]
    private UserService UserService { get; set; } = null!;

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

    [Inject]
    private IJSRuntime Js { get; set; } = null!;

    #endregion

    #region PROPERTIES

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
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "deputydirector";

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

        Users = (await UserService.GetAllAsync() ?? Array.Empty<User>()).Where(u => u.Role == EnumRoles.DeputyDirector)
            .ToList();

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Create deputy director as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CreateDeputyDirectorAsync()
    {
        _isLoading = true;
        StateHasChanged();

        var response = await UserService.CreateAsync(new User());
        
        if(response.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"{NavigateToUri}/{JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync())!.Id}");
        else
            await MessageService.Error(response.ReasonPhrase);

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Updates the list.
    /// </summary>
    /// <param name="division">The division.</param>
    private async Task UpdateList(Division? division)
    {
        if (division != null)
        {
            _isLoading = true;
            StateHasChanged();

            if (division.Name != null)
            {
                Users = (await UserService.GetAllByFilterAsync(
                    new FilterParams
                    {
                        ColumnName = "DivisionId",
                        FilterOption = EnumFilterOptions.Contains,
                        FilterValue = division.Id.ToString()
                    }) ?? Array.Empty<User>()).Where(u => u.Role == EnumRoles.DeputyDirector).ToList();
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
                
            Users = (await UserService.GetAllByFilterAsync(query!) ?? Array.Empty<User>()).Where(u => u.Role == EnumRoles.DeputyDirector).ToList();

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Remove as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveAsync(User user)
    {
        var response = await UserService.DeleteAsync(user.Id);
        if (response.IsSuccessStatusCode)
            await MessageService.Success($"Пользователь {user.Name} успешно удален.");
        else
            await MessageService.Error(response.ReasonPhrase);

        await GetListAsync();

        NavigationManager.NavigateTo($"/{NavigateToUri}", true);
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="user">The user.</param>
    private void UpdateAsync(IHaveId user)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{user.Id}");
    }

    public async Task DownloadExcelDeputyDirectors()
    {
        var excelBytes = ExportDeputyDirectors();
        await Js.InvokeVoidAsync("saveAsFile", $"Export_Deputy_Directors_{DateTime.Now:d}.xlsx", Convert.ToBase64String(excelBytes));
    }

    private byte[] ExportDeputyDirectors()
    {
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            var workSheetDataDeputyDirectors = package.Workbook.Worksheets.Add("Заместители директоров");

            using (var cells = workSheetDataDeputyDirectors.Cells["A1:E1"])
            {
                cells.Merge = true;
                cells.Value = "Заместители директоров КНИТУ-КАИ";
            }

            workSheetDataDeputyDirectors.Cells[2, 1].Value = "№ п/п";
            workSheetDataDeputyDirectors.Cells[2, 2].Value = "ФИО";
            workSheetDataDeputyDirectors.Cells[2, 3].Value = "Подразделение";
            workSheetDataDeputyDirectors.Cells[2, 4].Value = "Телефон";
            workSheetDataDeputyDirectors.Cells[2, 5].Value = "Email";

            workSheetDataDeputyDirectors.Cells["A1:E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheetDataDeputyDirectors.Cells["A1:H2"].Style.Font.Bold = true;

            workSheetDataDeputyDirectors.Cells["A1:A1000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            var startRowId = 3;
            var numberOfTheDeputyDirector = 0;
            foreach (var user in Users!)
            {
                if (user.Role == EnumRoles.DeputyDirector)
                {
                    workSheetDataDeputyDirectors.Cells[startRowId, 2].Value = user.Name;
                    workSheetDataDeputyDirectors.Cells[startRowId, 3].Value = user.Division.Name;
                    workSheetDataDeputyDirectors.Cells[startRowId, 4].Value = user.Phone;
                    workSheetDataDeputyDirectors.Cells[startRowId, 5].Value = user.Email;

                    if (workSheetDataDeputyDirectors.Cells[startRowId, 2].Value != null)
                    {
                        numberOfTheDeputyDirector++;
                        workSheetDataDeputyDirectors.Cells[startRowId, 1].Value = numberOfTheDeputyDirector;
                    }

                    startRowId++;
                }
                else
                {
                    using (var cells = workSheetDataDeputyDirectors.Cells["A2:E2"])
                    {
                        cells.Merge = true;
                        cells.Value = "Данные не найдены";
                    }
                }
            }

            workSheetDataDeputyDirectors.Cells["A1:H100"].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }

}