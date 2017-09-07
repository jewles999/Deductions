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
        public void Should_ThrowBadInputException_If_NamesNotProvided(string FirstName, string LastName, decimal discount)
        {
            var ex = Assert.Throws<ArgumentException>(
                    () => _p.CalculateLetterADiscount(FirstName, LastName, discount)
                );
            StringAssert.StartsWith("Error Calculating Discount", ex.Message);
        }

        [Test]
        [TestCase("Amy", "Brown", 0.1)]
        [TestCase("Arnold","Amsterdam", 0.1)]
        [TestCase("Karen","Applewood", 0.1)]
        public void Should_Return_Discount_On_Either_Provided_Matches(string FirstName, string LastName, decimal discount)
        {
            var result = _p.CalculateLetterADiscount(FirstName, LastName, discount);
            Assert.That(result.Equals(discount));
        }

        [Test]
        [TestCase("Bob", "Brown", 0.1)]
        public void Should_Return_0_On_No_Matches(string FirstName, string LastName, decimal discount)
        {
            var result = _p.CalculateLetterADiscount(FirstName, LastName, discount);
            Assert.That(result.Equals(0));
        }

        [Test]
        [TestCase(0,-1)]
        public void Discount_Should_Not_Be_Negative(decimal amount, decimal discount)
        {
            var ex = Assert.Throws<ArgumentException>(
                    () => _p.CalculateSubTotal(amount, discount)
                );
            StringAssert.Contains("negative", ex.Message.ToLower());
        }

        [Test]
        [TestCase(-1, 1)]
        [TestCase(0, 1)]
                [TestCase(0,0)]
        public void Amount_Should_Be_Greater_Than_0(decimal amount, decimal discount)
        {
            var ex = Assert.Throws<ArgumentException>(
                    () => _p.CalculateSubTotal(amount, discount)
                );
            StringAssert.Contains("greater than zero", ex.Message.ToLower());
        }

        [Test]
        [TestCase(1, 2)]
        public void Amount_Should_Be_Greater_Than_Discount(decimal amount, decimal discount)
        {
            var ex = Assert.Throws<ArgumentException>(
                    () => _p.CalculateSubTotal(amount, discount)
                );
            StringAssert.Contains("greater than discount", ex.Message.ToLower());
        }

        [Test]
        [TestCase(1,0)]
        [TestCase(1,1)]
        public void Subtotal_Should_Return_Positive_Value_Or_Amount(decimal amount, decimal discount)
        {
            var subtotal = _p.CalculateSubTotal(amount, discount);
            Assert.That(subtotal >= 0);
        }
    }
}
