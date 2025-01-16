using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Featuressection
{
    public decimal Featuressectionid { get; set; }

    public string Name { get; set; } = null!;

    public string Firstlefttext { get; set; } = null!;

    public string? Secoundlefttext { get; set; }

    public string Thirdlefttext { get; set; } = null!;

    public string Button { get; set; } = null!;
}
