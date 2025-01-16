using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Schedule
{
    public decimal Scheduleid { get; set; }

    public decimal Classtypeid { get; set; }

    public decimal Trainersid { get; set; }

    public bool? Isactive { get; set; }

    public virtual Classtype Classtype { get; set; } = null!;

    public virtual Trainer Trainers { get; set; } = null!;
}
