using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deductions.Business
{
    public class Paycheck : ILetterADiscount
    {
        public decimal CalculateDiscountedDeduction(decimal Deduction, decimal Discount)
        {
            return Deduction * Discount;
        }

        /// <summary>
        /// If FirstName or LastName starts with A, return discount
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Discount"></param>
        /// <returns>Discount or 0</returns>
        public decimal CalculateLetterADiscount(string FirstName, string LastName, decimal Discount)
        {
            if (String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName))
                throw new ArgumentException("Error Calculating Discount! First and Last names should be provided!");

            return FirstName.Substring(0, 1).ToUpper().Equals("A")
                || LastName.Substring(0, 1).ToUpper().Equals("A") ? Discount : 0;
        }

        public decimal CalculateSubTotal(decimal Amount, decimal Discount)
        {
            return Amount >= Discount && Discount >= 0 ? Amount - Discount : Amount;
        }
    }
}
