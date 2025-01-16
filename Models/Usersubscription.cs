using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Usersubscription
{
    public decimal Subscriptionid { get; set; }

    public decimal Userid { get; set; }

    public decimal Planid { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public virtual Subscriptionplan Plan { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
