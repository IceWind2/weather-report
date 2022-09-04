using WeatherReport.Core.WeatherDataRequest.RequestBuilders;

namespace WeatherReport.Core.WeatherDataRequest;

public class DataParser
{
    private IHttpClientFactory _clientFactory;
    private IRequestBuilder _requestBuilder;

    public DataParser(IHttpClientFactory clientFactory, IRequestBuilder requestBuilder)
    {
        _clientFactory = clientFactory;
        _requestBuilder = requestBuilder;
    }

    public string Parse()
    {
        var client = _clientFactory.CreateClient();

        var request = BuildRequest();
        
        return client.GetStringAsync(request).Result;
    }

    private string BuildRequest()
    {
        _requestBuilder.Reset();
        _requestBuilder.SetCoordinates(52.52, 13.41);
        _requestBuilder.SetTimeInterval(TimeInterval.Daily);
        _requestBuilder.UseTemperature();
        return _requestBuilder.BuildRequest();
    }
}