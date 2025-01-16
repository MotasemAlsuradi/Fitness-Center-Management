using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Classtype
{
    public decimal Classtypeid { get; set; }

    public string Classname { get; set; } = null!;

    public string? Dayofweek { get; set; }

    public DateTime Starttime { get; set; }

    public DateTime Endtime { get; set; }

    public string? Classdescription { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Trainerclass> Trainerclasses { get; set; } = new List<Trainerclass>();
}
