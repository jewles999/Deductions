using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deductions.Business
{
    public interface ILetterADiscount
    {
        decimal CalculateLetterADiscount(string firstName, string lastName, decimal discount);
        decimal CalculateDiscountedDeduction(decimal deduction, decimal discount);
        decimal CalculateSubTotal(decimal amount, decimal discount);
        decimal CalculatePayPeriodValue(decimal subtotal, int numberOfPayPeriods);
    }
}
