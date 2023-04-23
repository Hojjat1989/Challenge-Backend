using System;

namespace MartinDelivery.Api.Auth;

public interface IServiceWithOrganization
{
    public int OrganizationId { get; set; }
}
