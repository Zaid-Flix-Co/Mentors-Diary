using MentorsDiary.Application.Entities.Users.Domains;
using System;
using System.Windows;

namespace MentorsDiary.Desktop.Admin;

/// <summary>
/// Class UserEditorWindow.
/// Implements the <see cref="Window" />
/// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
/// </summary>
/// <seealso cref="Window" />
/// <seealso cref="System.Windows.Markup.IComponentConnector" />
public partial class UserEditorWindow : Window
{
    /// <summary>
    /// Occurs when [user added].
    /// </summary>
    public event EventHandler<UserEventArgs> UserAdded;

    /// <summary>
    /// Occurs when [user updated].
    /// </summary>
    public event EventHandler<UserEventArgs> UserUpdated;

    /// <summary>
    /// Occurs when [user edited].
    /// </summary>
    public event EventHandler<UserEventArgs> UserEdited;

    /// <summary>
    /// The user
    /// </summary>
    private User _user;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserEditorWindow"/> class.
    /// </summary>
    public UserEditorWindow()
    {
        InitializeComponent();
        idTextBox.IsEnabled = false;
        Title = "Новый пользователь";
        _user = new User();
        DataContext = _user;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserEditorWindow"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    public UserEditorWindow(User user)
    {
        InitializeComponent();
        idTextBox.IsEnabled = true;
        Title = $"Пользователь {user.Name}";
        _user = user;
        DataContext = _user;
    }

    /// <summary>
    /// Handles the Click event of the SaveButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_user.Id.ToString()))
        {
            MessageBox.Show("Please enter a valid ID.", "Invalid ID", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(_user.Name))
        {
            MessageBox.Show("Please enter a valid name.", "Invalid Name", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(_user.Password))
        {
            MessageBox.Show("Please enter a valid password.", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (UserAdded != null)
        {
            UserAdded(this, new UserEventArgs(_user));
        }
        else if (UserUpdated != null)
        {
            UserUpdated(this, new UserEventArgs(_user));
        }

        OnUserEdited(_user);

        Close();
    }

    /// <summary>
    /// Called when [user edited].
    /// </summary>
    /// <param name="user">The user.</param>
    private void OnUserEdited(User user)
    {
        UserEdited?.Invoke(this, new UserEventArgs(user));
    }
}

/// <summary>
/// Class UserEventArgs.
/// Implements the <see cref="EventArgs" />
/// </summary>
/// <seealso cref="EventArgs" />
public class UserEventArgs : EventArgs
{
    /// <summary>
    /// Gets the user.
    /// </summary>
    /// <value>The user.</value>
    public User User { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserEventArgs"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    public UserEventArgs(User user)
    {
        User = user;
    }
}