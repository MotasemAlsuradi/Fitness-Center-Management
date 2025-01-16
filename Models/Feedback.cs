using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Feedback
{
    public decimal Feedbackid { get; set; }

    public decimal Userid { get; set; }

    public string? Comments { get; set; }

    public DateTime? Userregistrationdate { get; set; }

    public bool? Isactive { get; set; }

    public virtual User User { get; set; } = null!;
}
