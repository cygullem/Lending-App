using System;
using System.Collections.Generic;

namespace CyMvc.Entities;

public partial class Transaction
{
    public int Id { get; set; }

    public decimal PaymentId { get; set; }

    public int LoanId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
}
