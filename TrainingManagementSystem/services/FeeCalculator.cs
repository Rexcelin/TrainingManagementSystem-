
using System;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class FeeCalculator
    {
        public (decimal BaseFee, decimal TotalDiscount, decimal FinalFee)
            CalculateFee(decimal baseFee, decimal surcharge)
        {
            if (baseFee < 0 || surcharge < 0)
            {
                throw new ArgumentException(
                    "Fee and surcharge cannot be negative.");
            }

            return (baseFee, 0, baseFee + surcharge);
        }

        public (decimal BaseFee, decimal TotalDiscount, decimal FinalFee)
            CalculateFee(decimal baseFee, decimal earlyDiscount,
                decimal groupDiscount, decimal surcharge)
        {
            if (baseFee < 0 || earlyDiscount < 0 ||
                groupDiscount < 0 || surcharge < 0)
            {
                throw new ArgumentException(
                    "Fees and discounts cannot be negative.");
            }

            decimal totalDiscount = earlyDiscount + groupDiscount;

            if (totalDiscount > baseFee)
            {
                throw new ArgumentException(
                    "Total discount cannot exceed the base fee.");
            }

            decimal finalFee = baseFee - totalDiscount + surcharge;

            return (baseFee, totalDiscount, finalFee);
        }

        public (decimal EarlyBirdDiscount, decimal GroupDiscount)
            CalculateDiscounts(decimal baseFee, DateTime enrollmentDate,
                DateTime courseStartDate, int eligibleLearnerCount)
        {
            if (baseFee < 0 || eligibleLearnerCount < 0)
            {
                throw new ArgumentException(
                    "Fee and learner count cannot be negative.");
            }

            decimal earlyDiscount = 0;
            decimal groupDiscount = 0;

            if ((courseStartDate - enrollmentDate).TotalDays >= 14)
            {
                earlyDiscount = baseFee * 0.10m;
            }

            if (eligibleLearnerCount >= 3)
            {
                groupDiscount = baseFee * 0.05m;
            }

            return (earlyDiscount, groupDiscount);
        }

        public void DisplayFeeBreakdown(in FeeSummary summary)
        {
            Console.WriteLine("\n===== FEE BREAKDOWN =====");
            Console.WriteLine($"Base Fee:             Rs. {summary.BaseFee:F2}");
            Console.WriteLine($"Early-Bird Discount: -Rs. {summary.EarlyBirdDiscount:F2}");
            Console.WriteLine($"Group Discount:      -Rs. {summary.GroupDiscount:F2}");
            Console.WriteLine($"Total Discount:      -Rs. {summary.TotalDiscount:F2}");
            Console.WriteLine($"Session Surcharge:    Rs. {summary.Surcharge:F2}");
            Console.WriteLine($"Final Fee:             Rs. {summary.FinalFee:F2}");
        }
    }
}
