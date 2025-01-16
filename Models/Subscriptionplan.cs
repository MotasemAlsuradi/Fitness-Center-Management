using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Subscriptionplan
{
    public decimal Planid { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Descripelineone { get; set; } = null!;

    public string? Descripelinetwo { get; set; }

    public string? Descripelinethree { get; set; }

    public string? Descripelinefour { get; set; }

    public string? Descripelinefive { get; set; }

    public string? Descripelinesix { get; set; }

    public string? Descripelineseven { get; set; }

    public string? Descripelineeghit { get; set; }

    public string? Descripelinenine { get; set; }

    public string Advicetext { get; set; } = null!;

    public string Buttontext { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Usersubscription> Usersubscriptions { get; set; } = new List<Usersubscription>();
}
