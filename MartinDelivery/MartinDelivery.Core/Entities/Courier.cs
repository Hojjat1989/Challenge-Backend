using System;
using System.ComponentModel.DataAnnotations.Schema;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities;

[Table("Courier")]
public class Courier : EntityBase
{
    public string Name { get; set; }
    public string Phone { get; set; }
}
