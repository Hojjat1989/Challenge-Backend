using System;

namespace MartinDelivery.Api.Auth;

public class OrganizationTokenModel
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

    public static OrganizationTokenModel FromDictionary(Dictionary<string, string> map)
    {
        return new OrganizationTokenModel
        {
            Id = int.Parse(map[IdKeyName])
        };
    }
}
