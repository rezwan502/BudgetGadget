using BudgetGadget.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetGadget.ViewModel
{
    public class EarningExpenseViewModel
    {
        public Earning Earning { get; set; }
        public Expense Expense { get; set; }

        public Registration Registration { get; set; }
        public int RegistrationId { get; set; }
    }
}