using System;
using System.ComponentModel.DataAnnotations.Schema;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities;

[Table("Organization")]
public class Organization : EntityBase
{
    public string Name { get; set; }
}
