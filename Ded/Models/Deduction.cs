using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DeductionsAPI.Models
{
    /// <summary>
    /// A Model and a Static Singleton of Deduction settings from the database
    /// </summary>
    public class Deduction
    {
        [Key]
        public int Id { get; set; }
        public decimal EmployeeDeduction { get; set; }
        public decimal DependentDeduction { get; set; }
        public decimal Discount { get; set; }
        public int NumberOfPayPeriods { get; set; }
    }

    public static class DeductionContstants
    {
        public static decimal EmployeeDeduction;
        public static decimal DependentDeduction;
        public static decimal Discount;
        public static int NumberOfPayPeriods;

        static DeductionContstants()
        {
            //fill config vars from the database
            using (var db = new ApplicationDbContext())
            {
                var ded = db.Deductions.FirstOrDefault();

                EmployeeDeduction = ded.EmployeeDeduction;
                DependentDeduction = ded.DependentDeduction;
                Discount = ded.Discount;
                NumberOfPayPeriods = ded.NumberOfPayPeriods;
            }
        }
    }
}