namespace Deductions.Business
{
    public interface IPaycheck
    {
        decimal CalculateLetterADiscount(string firstName, string lastName, decimal discount);
        decimal CalculateDiscountedDeduction(decimal deduction, decimal discount);
        decimal CalculateSubTotal(decimal amount, decimal discount);
        decimal CalculatePayPeriodValue(decimal subtotal, int numberOfPayPeriods);
        decimal Round(decimal value);
    }
}
