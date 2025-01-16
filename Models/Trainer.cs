using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Center_Management.Models;

public partial class Trainer
{
    public decimal Trainersid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Expertise { get; set; }

    public string? Picture { get; set; }

    [NotMapped]
    public virtual IFormFile? PictureUrl { get; set; }

    public string? Socialfacebook { get; set; }

    public string? Socialtwitter { get; set; }

    public string? Sociallinkedin { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Trainerclass> Trainerclasses { get; set; } = new List<Trainerclass>();
}
