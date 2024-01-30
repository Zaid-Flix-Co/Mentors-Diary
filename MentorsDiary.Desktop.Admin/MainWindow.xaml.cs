using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Desktop.Admin.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MentorsDiary.Desktop.Admin;

/// <summary>
/// Class MainWindow.
/// Implements the <see cref="Window" />
/// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
/// </summary>
/// <seealso cref="Window" />
/// <seealso cref="System.Windows.Markup.IComponentConnector" />
public partial class MainWindow : Window
{
    /// <summary>
    /// The user service
    /// </summary>
    private readonly UserService _userService;

    /// <summary>
    /// The user editor window
    /// </summary>
    private UserEditorWindow _userEditorWindow;

    /// <summary>
    /// The current page
    /// </summary>
    private int _currentPage = 1;

    /// <summary>
    /// The page size
    /// </summary>
    private int _pageSize = 10;

    /// <summary>
    /// The displayed users
    /// </summary>
    private List<User> _displayedUsers;

    /// <summary>
    /// The users
    /// </summary>
    private IEnumerable<User> _users;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow" /> class.
    /// </summary>
    /// <param name="userService">The user service.</param>
    public MainWindow(UserService userService)
    {
        InitializeComponent();
        _userService = userService;

        _ = UpdateDisplayedUsers();
    }

    /// <summary>
    /// Updates the displayed users.
    /// </summary>
    private async Task UpdateDisplayedUsers()
    {
        var startIndex = (_currentPage - 1) * _pageSize;

        _users = await _userService.GetAllAsync() ?? Array.Empty<User>();

        _displayedUsers = _users.Skip(startIndex).Take(_pageSize).ToList();
        userDataGrid.ItemsSource = _displayedUsers;

        var totalPages = GetTotalPages();
        var totalRecords = GetTotalRecords();

        totalPagesLabel.Content = $"Всего страниц: {totalPages}";
        currentPageLabel.Content = $"Текущая страница: {_currentPage}";
        totalRecordsLabel.Content = $"Всего записей: {totalRecords}";
    }

    /// <summary>
    /// Handles the Click event of the NextButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private async void NextButton_Click(object sender, RoutedEventArgs e)
    {
        var maxPage = (int)Math.Ceiling((double)(_users ?? Array.Empty<User>()).Count() / _pageSize);
        if (_currentPage < maxPage)
        {
            _currentPage++;
            await UpdateDisplayedUsers();
        }
    }

    /// <summary>
    /// Handles the Click event of the PreviousButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private async void PreviousButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentPage > 1)
        {
            _currentPage--;
            await UpdateDisplayedUsers();
        }
    }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    /// <returns>System.Int32.</returns>
    private int GetTotalPages()
    {
        return (int)Math.Ceiling((double)_users.Count() / _pageSize);
    }

    /// <summary>
    /// Gets the total records.
    /// </summary>
    /// <returns>System.Int32.</returns>
    private int GetTotalRecords()
    {
        return _users.Count();
    }

    /// <summary>
    /// Handles the Click event of the AddUserButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
        _userEditorWindow = new UserEditorWindow();
        _userEditorWindow.UserAdded += UserEditorWindow_UserAdded!;
        _userEditorWindow.Show();
    }

    /// <summary>
    /// Handles the Click event of the EditUserButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private void EditUserButton_Click(object sender, RoutedEventArgs e)
    {
        if (userDataGrid.SelectedItem is User selectedUser)
        {
            _userEditorWindow = new UserEditorWindow(selectedUser);
            _userEditorWindow.UserUpdated += UserEditorWindow_UserUpdated!;
            _userEditorWindow.Show();
        }
        else
        {
            MessageBox.Show("Выберите пользователя.", "Не выбран пользователь для редактирования.", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    /// <summary>
    /// Handles the UserAdded event of the UserEditorWindow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="UserEventArgs" /> instance containing the event data.</param>
    private async void UserEditorWindow_UserAdded(object sender, UserEventArgs e)
    {
        await _userService.CreateAsync(e.User);
        _userEditorWindow.Close();
        await UpdateDisplayedUsers();
    }

    /// <summary>
    /// Handles the UserUpdated event of the UserEditorWindow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="UserEventArgs" /> instance containing the event data.</param>
    private async void UserEditorWindow_UserUpdated(object sender, UserEventArgs e)
    {
        await _userService.UpdateAsync(e.User);
        _userEditorWindow.Close();
        await UpdateDisplayedUsers();

        var users = (await _userService.GetAllAsync() ?? Array.Empty<User>()).ToList();

        var index = users.IndexOf(e.User);
        if (index != -1)
        {
            users[index] = e.User;
            userDataGrid.Items.Refresh();
        }
    }

    /// <summary>
    /// Handles the Click event of the DeleteUserButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
    {
        if (userDataGrid.SelectedItem is User selectedUser)
        {
            if(MessageBox.Show("Подтверждение.", $"Вы действительно хотите удалить пользователя {selectedUser.Name}?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var response = await _userService.DeleteAsync(selectedUser.Id);
                if (response.IsSuccessStatusCode)
                {
                    await UpdateDisplayedUsers();
                    MessageBox.Show("Пользователь удален.", $"Пользователь {selectedUser.Name} удален.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(response.ReasonPhrase);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите пользователя.", "Не выбран пользователь для удаления.", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    /// <summary>
    /// Handles the Closed event of the Window control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void Window_Closed(object sender, EventArgs e)
    {
        System.Windows.Application.Current.Dispatcher.InvokeShutdown();
    }
}