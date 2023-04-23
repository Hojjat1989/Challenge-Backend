using System;

namespace MartinDelivery.Api.Auth;

public class CourierTokenModel
{
    private const string IdKeyName = "id";

    public int Id { get; set; }

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { IdKeyName, Id.ToString() }
        };
    }

    public static CourierTokenModel FromDictionary(Dictionary<string, string> map)
    {
        return new CourierTokenModel
        {
            Id = int.Parse(map[IdKeyName])
        };
    }
}
