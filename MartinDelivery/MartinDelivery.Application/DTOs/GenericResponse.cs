using System;

namespace MartinDelivery.Application.DTOs;

public class GenericResponse
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
}
