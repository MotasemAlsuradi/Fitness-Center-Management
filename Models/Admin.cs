using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Center_Management.Models;

public partial class Admin
{
    public decimal Adminid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Picture { get; set; }

    [NotMapped]
    public virtual IFormFile? PictureUrl { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
