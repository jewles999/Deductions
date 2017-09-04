using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deductions.Business
{
    public interface ILetterADiscount
    {
        decimal CalculateLetterADiscount(string FirstName, string LastName, decimal Discount);
        decimal CalculateDiscountedDeduction(decimal Deduction, decimal Discount);
        decimal CalculateSubTotal(decimal Amount, decimal Discount);
    }
}
