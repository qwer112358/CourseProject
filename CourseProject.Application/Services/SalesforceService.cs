using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CourseProject.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace CourseProject.Application.Services;

public class SalesforceService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
{
    public async Task<string> AuthenticateAsync()
    {
        var client = httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, configuration["Salesforce:AuthUrl"]);

        var parameters = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", configuration["Salesforce:ClientId"] },
            { "client_secret", configuration["Salesforce:ClientSecret"] },
            { "username", configuration["Salesforce:Username"] },
            { "password", configuration["Salesforce:Password"] + configuration["Salesforce:Token"] }
        };

        request.Content = new FormUrlEncodedContent(parameters);

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(json);

        return tokenResponse.AccessToken;
    }

    public async Task CreateAccountAndContactAsync(string accessToken, string accountName, string contactName, string email)
    {
        var client = httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var account = new { Name = accountName };
        var accountContent = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");

        var accountResponse = await client.PostAsync($"{configuration["Salesforce:ApiUrl"]}/sobjects/Account", accountContent);
        var accountResult = await accountResponse.Content.ReadAsStringAsync();
        var accountId = JsonSerializer.Deserialize<CreateResponse>(accountResult).Id;

        var contact = new { LastName = contactName, Email = email, AccountId = accountId };
        var contactContent = new StringContent(JsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

        await client.PostAsync($"{configuration["Salesforce:ApiUrl"]}/sobjects/Contact", contactContent);
    }
}

