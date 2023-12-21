using IoT.Models;

namespace IoT.Data;

public static class MockData
{
  public static float BaseLatitude = 50.450001f;
  public static float BaseLongitude = 30.523333f;
  public static float BaseTemperature = 25.0f;
  public static float BaseHumidity = 60.0f;
  private static readonly Random random = new Random();

  // Метод для получения фейковых данных о местоположении груза
  public static TransferLocation GetRandomTransferLocation()
  {
    float latitude = BaseLatitude + GetRandomOffset();
    float longitude = BaseLongitude + GetRandomOffset();

    return new TransferLocation
    {
      Latitude = latitude,
      Longitude = longitude
    };
  }

  // Метод для получения фейковых данных о состоянии груза
  public static TransferConditionModel GetRandomTransferCondition()
  {
    float temperature = BaseTemperature + GetRandomDeviation();
    float humidity = BaseHumidity + GetRandomDeviation();

    return new TransferConditionModel
    {
      Temperature = temperature,
      Humidity = humidity
    };
  }

  private static float GetRandomOffset()
  {
    return (float)(random.NextDouble() * 0.1 - 0.05);
  }

  private static float GetRandomDeviation()
  {
    return (float)(random.NextDouble() * 5.0 - 2.5);
  }
}
