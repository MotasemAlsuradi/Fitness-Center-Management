using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Center_Management.Models;

public partial class User
{
    public decimal Userid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public decimal Age { get; set; }

    public string? Gender { get; set; }

    public string? Picture { get; set; }

    [NotMapped]
    public virtual IFormFile? PictureUrl { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? Registrationdate { get; set; }

    public virtual ICollection<Bank> Banks { get; set; } = new List<Bank>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Usersubscription> Usersubscriptions { get; set; } = new List<Usersubscription>();
}
