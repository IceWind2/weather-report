namespace WeatherReport.Core.WeatherDataRequest.RequestBuilders;

public interface IRequestBuilder
{
    void Reset();
    
    void SetLatitude(double value);

    void SetLongitude(double value);

    void SetCoordinates(double latitude, double longitude);

    void SetTimeInterval(TimeInterval timeInterval);
    
    void UseTemperature();

    void UseWind();

    void UseHumidity();

    void UsePressure();
    
    string BuildRequest();
}