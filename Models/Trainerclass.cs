using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Trainerclass
{
    public decimal Trainerclassesid { get; set; }

    public decimal? Trainersid { get; set; }

    public decimal? Classtypeid { get; set; }

    public virtual Classtype? Classtype { get; set; }

    public virtual Trainer? Trainers { get; set; }
}
