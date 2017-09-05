using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Discount"></param>
        /// <returns>Discount or 0</returns>
        public decimal CalculateLetterADiscount(string firstName, string lastName, decimal discount)
        {
            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName))
                throw new ArgumentException("Error Calculating Discount! First and Last names should be provided!");

            return firstName.Substring(0, 1).ToUpper().Equals("A")
                || lastName.Substring(0, 1).ToUpper().Equals("A") ? discount : 0;
        }

        /// <summary>
        /// Cost of benefit per pay period
        /// </summary>
        /// <param name="subtotal"></param>
        /// <param name="numberOfPayPeriods"></param>
        /// <returns>Decimal</returns>
        public decimal CalculatePayPeriodValue(decimal subtotal, int numberOfPayPeriods)
        {
            if (numberOfPayPeriods <= 0)
                throw new ArgumentException("Number of pay periods should be greater than zero!");

            return subtotal / numberOfPayPeriods;
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
                throw new ArgumentException("Discount value cannot be negative!");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero!");

            return amount - discount;
        }

        
    }
}
