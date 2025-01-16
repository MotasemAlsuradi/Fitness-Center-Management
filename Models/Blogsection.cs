using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Blogsection
{
    public decimal Blogsectionid { get; set; }

    public string Name { get; set; } = null!;

    public string Tiptitle { get; set; } = null!;

    public string? Firsttiptext { get; set; }

    public string? Secoundtiptext { get; set; }
}
