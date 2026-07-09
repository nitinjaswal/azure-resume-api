using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ResumeApi;

public class GetResume
{
    private readonly Container _container;
    private readonly ILogger<GetResume> _logger;

    public GetResume(CosmosClient cosmosClient, ILogger<GetResume> logger)
    {
        _container = cosmosClient.GetContainer("ResumeDb", "Resumes");
        _logger = logger;
    }

    [Function("GetResume")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "resume")] HttpRequestData req)
    {
        try
        {
            var response = await _container.ReadItemAsync<Resume>(
                id: "1",
                partitionKey: new PartitionKey("1"));

            Resume resume = response.Resource;
            resume.visitor_count = resume.visitor_count + 1;

            await _container.ReplaceItemAsync(resume, "1", new PartitionKey("1"));

            string json = JsonConvert.SerializeObject(resume, Formatting.Indented);

            var httpResponse = req.CreateResponse(HttpStatusCode.OK);
            httpResponse.Headers.Add("Content-Type", "application/json");
            await httpResponse.WriteStringAsync(json);
            return httpResponse;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Resume document not found");
            return req.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}