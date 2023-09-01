using OrderManagement.Application.Features.Auth.Queries.AuthorizeUser;
using System.Text.Json.Serialization;

namespace OrderManagement.Test.Common;

public class BaseTest
{
    protected HttpClient _client;
    protected TestingWebApiFactory<Program> _factory;
    protected const string baseUri = "/api/";
    protected readonly string? _controllerName;
    private readonly AuthorizeUserQuery _authorizeUserQuery;
    public BaseTest(string? controllerName, string? userName, string? password)
    {
        _factory = new TestingWebApiFactory<Program>();
        _controllerName = controllerName;
        _client = _factory.CreateClient();
        _authorizeUserQuery = new AuthorizeUserQuery
        {
            UserName = userName,
            Password = password
        };
    }

    public async Task<TResponse> Get<TResponse>(string? route = null, bool ensureSuccessStatusCode = false)
    {
        await SetUpAuth();
        var response = await _client.GetAsync($"{baseUri}{_controllerName}{(string.IsNullOrEmpty(route) ? string.Empty : "/" + route)}");
        return await ProcessResponse<TResponse>(response, ensureSuccessStatusCode);
    }

    public async Task<TResponse> Post<TRequest, TResponse>(TRequest value, string? controllerName = null, string? route = null, bool ensureSuccessStatusCode = false)
    {
        await SetUpAuth();
        var response = await _client.PostAsJsonAsync(GetRoute(controllerName, route), value);
        return await ProcessResponse<TResponse>(response, ensureSuccessStatusCode);
    }

    private string GetRoute(string? controllerName = null, string? route = null, string? value = null)
    {
        return $"{baseUri}{(string.IsNullOrEmpty(controllerName) ? _controllerName : controllerName)}{(string.IsNullOrEmpty(route) ? string.Empty : "/" + route)}{(string.IsNullOrEmpty(value) ? string.Empty : "/" + value)}";
    }

    private async Task<TResponse> ProcessResponse<TResponse>(HttpResponseMessage response, bool ensureSuccessStatusCode)
    {
        if (ensureSuccessStatusCode)
            response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase), new JsonStringEnumConverter() }
        };

        return JsonSerializer.Deserialize<TResponse>(json, options);
    }

    private async Task SetUpAuth()
    {
        var tokenResponse = await _client.PostAsJsonAsync(GetRoute("Auth", "login"), _authorizeUserQuery);
        var authorizeUserResponse = await ProcessResponse<AuthorizeUserResponse>(tokenResponse, false);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizeUserResponse.AccessToken);
    }
}
