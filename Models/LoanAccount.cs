﻿using System.ComponentModel;

namespace App.Models
{
    public class LoanAccount 
    {
        public LoanAccount()
        {
            RepaymentFrequencyMonths = 1;
            StartDate = DateTime.Now;
        }

        [DisplayName("Linked Account")]
        public CustomerAccount CustomerAccount { get; set; }
        public int CustomerAccountId { get; set; }

        [DisplayName("Principal Amount")]
        public float Principal { get; set; }

        [DisplayName("% Interest Rate")]
        public double InterestRate { get; set; }

        [DisplayName("Duration in Years")]
        public float DurationYears { get; set; }

        [DisplayName("Compound Interest")]
        public double CompoundInterest { get; set; }

        [DisplayName("Repayment Amount Per Time")]
        public double RepaymentAmountPerTime { get; set; }

        [DisplayName("Repayment Frequency in Months")]
        public float RepaymentFrequencyMonths { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("Daily Accrual Balance")]
        public double AccrualBalance { get; set; }
    }
}
