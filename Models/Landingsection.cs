using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Center_Management.Models;

public partial class Landingsection
{
    public decimal Landingsectionid { get; set; }

    public string Name { get; set; } = null!;

    public string Mainimage { get; set; } = null!;

    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }

    public string Firstwelcometext { get; set; } = null!;

    public string? Secoundwelcometext { get; set; }

    public string Firstbutton { get; set; } = null!;

    public string Secoundbutton { get; set; } = null!;
}
