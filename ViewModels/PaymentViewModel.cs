using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CyMvc.ViewModels
{
    public partial class PaymentViewModel
    {
        public int PaymentId { get; set; }

        public int LoanId { get; set; }

        public int ClientId { get; set; }

        public decimal Collectable { get; set; }

        public decimal CollectableOrig { get; set; }

        public DateTime Date { get; set; }
    }
}