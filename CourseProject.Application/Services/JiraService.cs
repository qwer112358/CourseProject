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
        _apiToken = "ATATT3xFfGF0gczaNKxDUtxMamfRQlACyX0aVsjiP8E3TdswULlyGPQQ4n10mH6xiZMYx6h08pRRpWQfC1xx2JlN1Xe8GlrzJ3ZwxBnDs7qg805i0CDIl5M6dVNd2cf9F3WGMlI-TUjJH35hHdQ_uwKD64D-m3w_4YxFYEE6vGr-pGhus0DssmQ=E4F85A" + "89";
        _projectKey = config["Jira:ProjectKey"];
        _httpClient = httpClient;

        var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_email}:{_apiToken}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
    }

    public async Task<string> CreateTicketAsync(CreateTicketViewModel model, string userEmail)
    {
        await EnsureUserExistsAsync(userEmail);
        var accountId = await GetAccountIdAsync(userEmail);

        var jiraApiUrl = $"{_jiraBaseUrl}/rest/api/2/issue";
        var ticketData = new
        {
            fields = new
            {
                project = new { key = _projectKey },
                summary = model.Summary,
                description = $"Reported by: {userEmail}\nTemplate: {model.Template}\nPage link: {model.PageUrl}",
                issuetype = new { name = "Task" },
                priority = new { name = model.Priority },
                reporter = new { accountId = accountId },
                //customfield_10040 = model.Template
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
    
    public async Task<List<TicketViewModel>> GetTicketsByUserAsync(string userEmail)
    {
        var accountId = await GetAccountIdAsync(userEmail);
        var jqlQuery = $"reporter={accountId} AND project={_projectKey}";
        var jiraApiUrl = $"{_jiraBaseUrl}/rest/api/2/search?jql={Uri.EscapeDataString(jqlQuery)}";

        var response = await _httpClient.GetAsync(jiraApiUrl);
        if (response.IsSuccessStatusCode)
        {
            var responseData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            // Извлекаем данные и преобразуем их в TicketViewModel
            var tickets = new List<TicketViewModel>();
            foreach (var issue in responseData.issues)
            {
                var ticket = new TicketViewModel
                {
                    Key = issue.key,
                    Summary = issue.fields.summary,
                    Status = issue.fields.status.name,
                    Priority = issue.fields.priority.name,
                    Reporter = issue.fields.reporter.displayName,
                    Description = issue.fields.description
                };
                tickets.Add(ticket);
            }

            return tickets;
        }

        throw new Exception("Error fetching tickets for the user.");
    }
    
    private async Task EnsureUserExistsAsync(string userEmail)
    {
        var userResponse = await _httpClient.GetAsync($"{_jiraBaseUrl}/rest/api/3/user?username={userEmail}");
        if (!userResponse.IsSuccessStatusCode)
        {
            var newUser = new
            {
                emailAddress = userEmail,
                displayName = userEmail,
                name = userEmail,
                products = new[] { "jira-software" },
                password = "qwerty123@"
            };
            var userContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            var createUserResponse = await _httpClient.PostAsync($"{_jiraBaseUrl}/rest/api/3/user", userContent);
            
            if (!createUserResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create user in Jira.");
            }
        }
    }
    
    private async Task<string> GetAccountIdAsync(string userEmail)
    {
        var response = await _httpClient.GetAsync($"{_jiraBaseUrl}/rest/api/3/user/search?query={userEmail}");
        if (response.IsSuccessStatusCode)
        {
            var responseData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            if (responseData.Count > 0)
            {
                return responseData[0].accountId;
            }
        }
        throw new Exception("Unable to retrieve accountId for the user.");
    }

}


public class JiraSearchResult
{
    [JsonProperty("expand")]
    public string Expand { get; set; }

    [JsonProperty("startAt")]
    public int StartAt { get; set; }

    [JsonProperty("maxResults")]
    public int MaxResults { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("issues")]
    public List<JiraIssue> Issues { get; set; }
}

public class JiraIssue
{
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("fields")]
    public JiraIssueFields Fields { get; set; }
}

public class JiraIssueFields
{
    [JsonProperty("summary")]
    public string Summary { get; set; }

    [JsonProperty("status")]
    public JiraStatus Status { get; set; }

    [JsonProperty("priority")]
    public JiraPriority Priority { get; set; }

    [JsonProperty("created")]
    public string Created { get; set; }

    [JsonProperty("assignee")]
    public JiraUser Assignee { get; set; }

    [JsonProperty("reporter")]
    public JiraUser Reporter { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}

public class JiraStatus
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class JiraPriority
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class JiraUser
{
    [JsonProperty("displayName")]
    public string DisplayName { get; set; }
}
