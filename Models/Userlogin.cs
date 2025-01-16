using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Userlogin
{
    public decimal Userloginid { get; set; }

    public decimal Roleid { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
