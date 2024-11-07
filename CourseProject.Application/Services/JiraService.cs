using System.Net.Http.Headers;
using System.Text;
using CourseProject.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CourseProject.Application.Services;

public class JiraService
{
    private readonly string _jiraBaseUrl;
    private readonly string _email;
    private readonly string _apiToken;
    private readonly string _projectKey;
    private readonly HttpClient _httpClient;

    public JiraService(IConfiguration config, HttpClient httpClient)
    {
        _jiraBaseUrl = config["Jira:BaseUrl"];
        _email = config["Jira:Email"];
        _apiToken = config["Jira:ApiToken"];
        _projectKey = config["Jira:ProjectKey"];
        _httpClient = httpClient;

        var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_email}:{_apiToken}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
    }

    public async Task<string> CreateTicketAsync(CreateTicketViewModel model)
    {
        var jiraApiUrl = $"{_jiraBaseUrl}/rest/api/2/issue";
        var ticketData = new
        {
            fields = new
            {
                project = new { key = _projectKey },
                summary = model.Summary,
                description = $"Ticket created on the page: {model.PageUrl}",
                issuetype = new { name = "Task" },
                priority = new { name = model.Priority }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(ticketData), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(jiraApiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            var ticketKey = (string)responseData.key;
            return $"{_jiraBaseUrl}/browse/{ticketKey}";
        }

        throw new Exception("Error when creating a ticket in Jira");
    }
}