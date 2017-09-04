using System;
using Deductions.Business;
using NUnit.Framework;

namespace DeductionsAPI.Tests
{
    [TestFixture]
    public class PaycheckTests
    {
        private Paycheck _p;
        [SetUp]
        public void SetUp()
        {
            _p = new Paycheck();
        }

        [Test]
        [TestCase("","", 0.1)]
        public void Should_ThrowBadInputException_If_NamesNotProvided(string FirstName, string LastName, decimal Discount)
        {
            var ex = Assert.Throws<ArgumentException>(
                    () => _p.CalculateLetterADiscount(FirstName, LastName, Discount)
                );
            StringAssert.StartsWith("Error Calculating Discount", ex.Message);
        }

        [Test]
        [TestCase("Amy", "Brown", 0.1)]
        [TestCase("Arnold","Amsterdam", 0.1)]
        [TestCase("Karen","Applewood", 0.1)]
        public void Should_Return_Discount_On_Either_Provided_Matches(string FirstName, string LastName, decimal Discount)
        {
            decimal discount = _p.CalculateLetterADiscount(FirstName, LastName, Discount);
            Assert.That(discount.Equals(Discount));
        }

        [Test]
        [TestCase("Bob", "Brown", 0.1)]
        public void Should_Return_0_On_No_Matches(string FirstName, string LastName, decimal Discount)
        {
            decimal discount = _p.CalculateLetterADiscount(FirstName, LastName, Discount);
            Assert.That(discount.Equals(0));
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(1,0)]
        [TestCase(0,1)]
        [TestCase(1,1)]
        [TestCase(1,2)]
        public void Subtotal_Should_Return_Positive_Value_Or_Amount(decimal Amount, decimal Discount)
        {
            decimal subtotal = _p.CalculateSubTotal(Amount, Discount);
            Assert.That(subtotal >= 0);
        }
    }
}
