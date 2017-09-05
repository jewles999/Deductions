using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deductions.Business
{
    public class Paycheck : ILetterADiscount
    {
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
