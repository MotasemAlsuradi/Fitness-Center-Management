using System;
using System.Collections.Generic;

namespace Fitness_Center_Management.Models;

public partial class Payment
{
    public decimal Paymentid { get; set; }

    public decimal? Userid { get; set; }

    public decimal? Subscriptionid { get; set; }

    public DateTime? Paymentdate { get; set; }

    public virtual Subscriptionplan? Subscription { get; set; }

    public virtual User? User { get; set; }
}
