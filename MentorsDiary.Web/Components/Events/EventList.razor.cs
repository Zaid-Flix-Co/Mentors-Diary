using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Web.Components.Events;

/// <summary>
/// Class EventList.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class EventList
{
    #region INJECTIONS

    /// <summary>
    /// Gets or sets the event service.
    /// </summary>
    /// <value>The event service.</value>
    [Inject]
    private EventService EventService { get; set; } = null!;

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
    private IMessageService MessageService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the authentication service.
    /// </summary>
    /// <value>The authentication service.</value>
    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    [Inject]
    private IJSRuntime Js { get; set; } = null!;

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
    /// Gets or sets the events.
    /// </summary>
    /// <value>The events.</value>
    private List<Event>? Events { get; set; }

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "event";

    /// <summary>
    /// Gets the selected date time.
    /// </summary>
    /// <value>The selected date time.</value>
    public DateTime?[]? SelectedDateTime { get; private set; }

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

        Events = (await EventService.GetAllAsync() ?? Array.Empty<Event>()).ToList();

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Create event as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CreateEventAsync()
    {
        _isLoading = true;
        StateHasChanged();

        var response = await EventService.CreateAsync(new Event()
        {
            DateCreated = DateTime.Now,
            UserCreated = CurrentUser.Name
        });

        if (response.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"{NavigateToUri}/{JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync())!.Id}");
        else
            await MessageService.Error(response.ReasonPhrase);

        _isLoading = false;
        StateHasChanged();
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

            Events = (await EventService.GetAllByFilterAsync(query!) ?? Array.Empty<Event>()).ToList();

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Remove as an asynchronous operation.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveAsync(Event @event)
    {
        var response = await EventService.DeleteAsync(@event.Id);

        if (response.IsSuccessStatusCode)
            await MessageService.Success($"Событие {@event.Name} успешно удалено.");
        else
            await MessageService.Error(response.ReasonPhrase);

        await GetListAsync();

        NavigationManager.NavigateTo($"/{NavigateToUri}", true);
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="event">The event.</param>
    private void UpdateAsync(IHaveId @event)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{@event.Id}");
    }

    /// <summary>
    /// Updates the list.
    /// </summary>
    /// <param name="dateTimeRange">The <see cref="DateRangeChangedEventArgs" /> instance containing the event data.</param>
    private async Task UpdateList(DateRangeChangedEventArgs<DateTime?[]>? dateTimeRange)
    {
        SelectedDateTime = dateTimeRange?.Dates!;

        await GetListAsync();

        _isLoading = true; 
        StateHasChanged();

        if (SelectedDateTime[0] != null && SelectedDateTime[1] != null)
            Events = Events?.Where(d => d.DateEvent > SelectedDateTime[0] && d.DateEvent < SelectedDateTime[1]).ToList();

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Updates the list after clear date picker.
    /// </summary>
    private async Task UpdateListAfterClearDatePicker()
    {
        await GetListAsync();
    }

    public async Task DownloadExcelFileEvents()
    {
        var excelBytes = ExportEvents();
        await Js.InvokeVoidAsync("saveAsFile", $"Export_Events_{DateTime.Now:d}.xlsx", Convert.ToBase64String(excelBytes));
    }

    private byte[] ExportEvents()
    {
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            var workSheetDataEvents = package.Workbook.Worksheets.Add("Мероприятия");

            using (var cells = workSheetDataEvents.Cells["A1:E1"])
            {
                cells.Merge = true;
                cells.Value = "Основные мероприятия";
            }

            workSheetDataEvents.Cells[2, 1].Value = "№ п/п";
            workSheetDataEvents.Cells[2, 2].Value = "Наименование мероприятия";
            workSheetDataEvents.Cells[2, 3].Value = "Описание";
            workSheetDataEvents.Cells[2, 4].Value = "Дата проведения";

            workSheetDataEvents.Cells["A1:E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheetDataEvents.Cells["A1:H2"].Style.Font.Bold = true;

            var startRowId = 3;
            var numberOfTheEvent = 0;
            foreach (var Event in Events!)
            {
                workSheetDataEvents.Cells[startRowId, 2].Value = Event.Name;
                workSheetDataEvents.Cells[startRowId, 3].Value = Event.Comment;
                workSheetDataEvents.Cells[startRowId, 4].Value = Event.DateEvent != null ? Event.DateEvent.Value.ToString("d") : "";

                if (workSheetDataEvents.Cells[startRowId, 2].Value != null)
                {
                    numberOfTheEvent++;
                    workSheetDataEvents.Cells[startRowId, 1].Value = numberOfTheEvent;
                }

                startRowId++;
            }

            workSheetDataEvents.Cells["A1:H100"].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}