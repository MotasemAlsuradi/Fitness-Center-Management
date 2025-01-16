using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Bank
{
    public decimal Bankid { get; set; }

    public decimal Userid { get; set; }

    public decimal Balance { get; set; }

    public string Cardnumber { get; set; } = null!;

    public string Cardholdername { get; set; } = null!;

    public DateTime Expirydate { get; set; }

    public string Cvv { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
