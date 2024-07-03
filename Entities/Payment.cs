using System;
using System.Collections.Generic;

namespace CyMvc.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int LoanId { get; set; }

    public decimal Collectable { get; set; }

    public DateTime Schedule { get; set; }
}
