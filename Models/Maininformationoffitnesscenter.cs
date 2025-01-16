using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Maininformationoffitnesscenter
{
    public decimal Maininformationoffitnesscenterid { get; set; }

    public string Name { get; set; } = null!;

    public string Firstabouttext { get; set; } = null!;

    public string Secoundabouttext { get; set; } = null!;

    public string Thirdabouttext { get; set; } = null!;

    public string Openday { get; set; } = null!;

    public string? Closedday { get; set; }

    public string Worktime { get; set; } = null!;

    public string Testamoinaltext { get; set; } = null!;

    public string Welcomelocationtext { get; set; } = null!;

    public string Locationtext { get; set; } = null!;

    public string Locationsource { get; set; } = null!;

    public string Copyrighttext { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
