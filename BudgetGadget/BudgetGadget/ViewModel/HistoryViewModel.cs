using BudgetGadget.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetGadget.ViewModel
{
    public class HistoryViewModel
    {
        public IEnumerable<Earning> Earning { get; set; }
        public IEnumerable<Expense> Expense { get; set; }

        public string Source { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }

        public Registration Registration { get; set; }
        public int RegistrationId { get; set; }
    }
}