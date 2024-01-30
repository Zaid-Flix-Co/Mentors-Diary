using System.Security.Claims;
using HttpService.Services;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Data.Authentication;

/// <summary>
/// Class WebsiteAuthenticator.
/// Implements the <see cref="AuthenticationStateProvider" />
/// </summary>
/// <seealso cref="AuthenticationStateProvider" />
public class WebsiteAuthenticator : AuthenticationStateProvider
{
    /// <summary>
    /// The protected local storage
    /// </summary>
    private readonly ProtectedLocalStorage _protectedLocalStorage;

    /// <summary>
    /// The user service
    /// </summary>
    private readonly UserService _userService;

    /// <summary>
    /// The authentication service
    /// </summary>
    private readonly AuthenticationService _authenticationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebsiteAuthenticator" /> class.
    /// </summary>
    /// <param name="protectedLocalStorage">The protected local storage.</param>
    /// <param name="userService">The user service.</param>
    /// <param name="authenticationService">The authentication service.</param>
    public WebsiteAuthenticator(ProtectedLocalStorage protectedLocalStorage,
        UserService userService,
        AuthenticationService authenticationService)
    {
        _protectedLocalStorage = protectedLocalStorage;
        _userService = userService;
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Get authentication state as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;AuthenticationState&gt; representing the asynchronous operation.</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();

        try
        {
            var storedPrincipal = await _protectedLocalStorage.GetAsync<string>("identity");

            if (storedPrincipal.Success)
            {
                var user = JsonConvert.DeserializeObject<User>(storedPrincipal.Value!);
                _authenticationService.AuthorizedUser = user;

                var (userInDb, isLookUpSuccess) = await LookUpUserAsync(user?.Name, user?.Password);

                if (isLookUpSuccess)
                {
                    var identity = CreateIdentityFromUserAsync(userInDb);
                    principal = new ClaimsPrincipal(await identity);
                }
            }
        }
        catch
        {
            // ignored
        }

        return new AuthenticationState(principal);
    }

    /// <summary>
    /// Login as an asynchronous operation.
    /// </summary>
    /// <param name="loginFormModel">The login form model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task<bool> LoginAsync(LoginFormModel loginFormModel)
    {
        var (userInDatabase, isSuccess) = await LookUpUserAsync(loginFormModel.Username, loginFormModel.Password);

        var principal = new ClaimsPrincipal();

        if (isSuccess)
        {
            var identity = CreateIdentityFromUserAsync(userInDatabase);
            principal = new ClaimsPrincipal(await identity);
            var ret = JsonConvert.SerializeObject(userInDatabase);
            await _protectedLocalStorage.SetAsync("identity", ret);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        return isSuccess;
    }

    /// <summary>
    /// Logout as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task LogoutAsync()
    {
        _authenticationService.AuthorizedUser = null;

        await _protectedLocalStorage.DeleteAsync("identity");
        var principal = new ClaimsPrincipal();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    /// <summary>
    /// Create identity from user as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A Task&lt;ClaimsIdentity&gt; representing the asynchronous operation.</returns>
    private static Task<ClaimsIdentity> CreateIdentityFromUserAsync(User user)
    {
        var result = new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.Name, user.Name!),
            new (ClaimTypes.Hash, user.Password!),
            new (ClaimTypes.Role, user.Role.ToString() ?? string.Empty),
        }, "WebsiteAuthenticator");

        return Task.FromResult(result);
    }

    /// <summary>
    /// Look up user as an asynchronous operation.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns>A Task&lt;System.ValueTuple&gt; representing the asynchronous operation.</returns>
    private async Task<(User, bool)> LookUpUserAsync(string? username, string? password)
    {
        var users = await _userService?.GetAllAsync()!;
        var result = users!.FirstOrDefault(u => username == u.Name && password == u.Password);

        return (result, result is not null)!;
    }
}