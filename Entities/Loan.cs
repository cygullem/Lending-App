using System;
using System.Collections.Generic;

namespace CyMvc.Entities;

public partial class Loan
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string Type { get; set; } = null!;

    public decimal Amount { get; set; }

    public decimal Interest { get; set; }

    public decimal InterestAmount { get; set; }

    public int Term { get; set; }

    public decimal Deduct { get; set; }

    public decimal Recievable { get; set; }

    public decimal TotalPayable { get; set; }

    public decimal Collected { get; set; }

    public decimal Collectable { get; set; }

    public DateTime DateCreated { get; set; }
}
