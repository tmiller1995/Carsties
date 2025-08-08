using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Auction.Domain.Interfaces;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Auction.Infrastructure.ImageGeneration;

public sealed partial class OpenAiImageService : IAiImageService
{
    private readonly ILogger<OpenAiImageService> _logger;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _client;

    public OpenAiImageService(
        ILogger<OpenAiImageService> logger,
        IConfiguration configuration,
        HttpClient client)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "ILogger<T> must be provided");
        _configuration = configuration ??
                         throw new ArgumentNullException(nameof(configuration), "IConfiguration must be provided");
        _client = client ?? throw new ArgumentNullException(nameof(client), "HttpClient must be provided");
    }

    public async Task<ErrorOr<string>> GetOrCreateAsync(int year, string make, string model, string color,
        CancellationToken ct = default)
    {
        var cleanedMake = WhiteSpaceCleaner(make);
        var cleanedModel = WhiteSpaceCleaner(model);
        var cleanedColor = WhiteSpaceCleaner(color);

        var request = new ImageGenerationRequest
        {
            Model = "dall-e-3",
            N = 1,
            Prompt = BuildImageGenerationPrompt(year, make, model, color),
            Quality = "standard",
            ResponseFormat = "url",
            Size = "1792x1024",
            Style = "vivid",
            User = $"seed-{Environment.MachineName}"
        };

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _configuration["OPENAI_API_KEY"]);
        var httpResponse = await _client.PostAsJsonAsync("v1/images/generations", request, ct);
        if (!httpResponse.IsSuccessStatusCode)
        {
            return Error.Failure(description: httpResponse.ReasonPhrase!);
        }

        var openAiResponse = await httpResponse.Content.ReadFromJsonAsync<OpenAiImageResponse>(ct);
        var tmpUrl = openAiResponse?.Data[0].Url!;

        return await GetImageFromOpenAiAsync(tmpUrl, year, cleanedMake, cleanedModel, cleanedColor, ct);
    }

    private async Task<ErrorOr<string>> GetImageFromOpenAiAsync(string url, int year, string cleanedMake,
        string cleanedModel, string cleanedColor, CancellationToken ct)
    {
        try
        {
            _client.DefaultRequestHeaders.Clear();
            await using var remoteImage = await _client.GetStreamAsync(url, ct);

            var imageSlugName = $"{year}-{cleanedMake}-{cleanedModel}-{cleanedColor}.jpg";
            var imageDirectory = BuildPathToPublicFrontEndFolder(year, cleanedMake, cleanedModel, cleanedColor);
            var fullPath = Path.Combine(imageDirectory, imageSlugName);

            await using var fileWriter = File.Create(fullPath);
            await remoteImage.CopyToAsync(fileWriter, ct);

            return Path.Join("images", "vehicles", year.ToString(), cleanedMake, cleanedModel, cleanedColor,
                imageSlugName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve and save image from OpenAI");
            return Error.Failure(description: "Failed to save the generated image");
        }
    }

    private static string BuildImageGenerationPrompt(int year, string make, string model, string color)
    {
        return
            $"High quality studio photo of a {color} {year} {make} {model} front three-quarter view on a white seamless background.";
    }

    private static string BuildPathToPublicFrontEndFolder(int year, string make, string model, string color)
    {
        const string rootPathToPublicFrontEnd = @"..\..\react-frontend\public\images\vehicles";

        var directoryForImage = Path.Join(rootPathToPublicFrontEnd, year.ToString(), make, model, color);
        if (!Directory.Exists(directoryForImage))
        {
            Directory.CreateDirectory(directoryForImage);
        }

        return directoryForImage;
    }

    private static string WhiteSpaceCleaner(string input) => WhiteSpaceRegex().Replace(input.ToLowerInvariant(), "-");

    [GeneratedRegex(@"\W+")]
    private static partial Regex WhiteSpaceRegex();
}