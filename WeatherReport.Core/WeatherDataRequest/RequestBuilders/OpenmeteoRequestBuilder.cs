using System.Globalization;
using System.Text;

namespace WeatherReport.Core.WeatherDataRequest.RequestBuilders;

public class OpenmeteoRequestBuilder : IRequestBuilder
{
    private double _latitude, _longitude;
    private bool _temperature, _wind, _humidity, _pressure;
    private TimeInterval _timeInterval;
    private readonly StringBuilder _request;
    
    public OpenmeteoRequestBuilder()
    {
        _request = new StringBuilder();
        Reset();
    }

    public void Reset()
    {
        _latitude = 0;
        _longitude = 0;
        _timeInterval = TimeInterval.Daily;
        _temperature = false;
        _wind = false;
        _humidity = false;
        _pressure = false;
        _request.Clear();
        _request.Append(" https://api.open-meteo.com/v1/forecast?");
    }
    
    public void SetLatitude(double value)
    {
        _latitude = value;
    }

    public void SetLongitude(double value)
    {
        _longitude = value;
    }

    public void SetCoordinates(double latitude, double longitude)
    {
        _latitude = latitude;
        _longitude = longitude;
    }

    public void SetTimeInterval(TimeInterval timeInterval)
    {
        _timeInterval = timeInterval;
    }

    public void UseTemperature()
    {
        _temperature = true;
    }

    public void UseWind()
    {
        _wind = true;
    }

    public void UseHumidity()
    {
        _humidity = true;
    }

    public void UsePressure()
    {
        _pressure = true;
    }

    public string BuildRequest()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        
        _request.Append($"latitude={_latitude}&longitude={_longitude}");

        switch (_timeInterval)
        {
            case TimeInterval.Hourly:
                _request.Append("&hourly=");
                break;
            case TimeInterval.Daily:
                _request.Append("&daily=");
                break;
            default:
                throw new InvalidOperationException("Invalid time interval value");
        }

        if (_temperature)
        {
            _request.Append("temperature_2m,");
        }

        _request.Remove(_request.Length - 1, 1);

        return _request.ToString();
    }
}