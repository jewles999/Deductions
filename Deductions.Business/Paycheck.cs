using System;
using Ded.Settings;

namespace Deductions.Business
{
    /// <summary>
    /// Business logic for calculating paycheck entries
    /// </summary>
    public class Paycheck : ILetterADiscount
    {
        /// <summary>
        /// Benefit cost with discount applied
        /// </summary>
        /// <param name="deduction"></param>
        /// <param name="discount"></param>
        /// <returns>Decimal</returns>
        public decimal CalculateDiscountedDeduction(decimal deduction, decimal discount)
        {
            return deduction * discount;
        }

        /// <summary>
        /// If FirstName or LastName starts with A, return discount
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="discount"></param>
        /// <returns>Discount or 0</returns>
        public decimal CalculateLetterADiscount(string firstName, string lastName, decimal discount)
        {
            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName))
                throw new ArgumentException(message: "Error Calculating Discount! First and Last names should be provided!");

            return firstName.Substring(0, 1).ToUpper().Equals(Constants.LetterDiscount)
                || lastName.Substring(0, 1).ToUpper().Equals(Constants.LetterDiscount) ? discount : 0;
        }

        /// <summary>
        /// Cost of benefit per pay period
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberOfPayPeriods"></param>
        /// <returns>Decimal</returns>
        public decimal CalculatePayPeriodValue(decimal value, int numberOfPayPeriods)
        {
            if (numberOfPayPeriods <= 0)
                throw new ArgumentException(message: "Number of pay periods should be greater than zero!");

            return value / numberOfPayPeriods;
        }

        /// <summary>
        /// Benefit cost less discount
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="discount"></param>
        /// <returns>Decimal</returns>
        public decimal CalculateSubTotal(decimal amount, decimal discount)
        {
            if (discount < 0)
                throw new ArgumentException(message: "Discount value cannot be negative!");

            if (amount <= 0)
                throw new ArgumentException(message: "Amount must be greater than zero!");

            if(amount < discount)
                throw new ArgumentException(message: "Amount must be greater than discount!");

            return amount - discount;
        }

        /// <summary>
        /// Handle money rounding in a unified way
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Decimal</returns>
        public decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}