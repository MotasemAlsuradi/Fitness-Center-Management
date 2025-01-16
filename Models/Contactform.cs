using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Contactform
{
    public decimal Contactformid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Message { get; set; } = null!;
}
