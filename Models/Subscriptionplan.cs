using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Center_Management.Models;
// Used For Classes
public partial class Subscriptionplan
{
    public decimal Planid { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
    // Day 
    public string Descripelineone { get; set; } = null!;
    // Start Time
    public string? Descripelinetwo { get; set; }
    // End Time
    public string? Descripelinethree { get; set; }
    // Goals
    public string? Descripelinefour { get; set; }
    // Image
    public string? Descripelinefive { get; set; }

    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }
    public string? Descripelinesix { get; set; }

    public string? Descripelineseven { get; set; }

    public string? Descripelineeghit { get; set; }

    public string? Descripelinenine { get; set; }
    // Exercise routines
    public string Advicetext { get; set; } = null!;
    // Trainer Name
    public string Buttontext { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Usersubscription> Usersubscriptions { get; set; } = new List<Usersubscription>();
}
