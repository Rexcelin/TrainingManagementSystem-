
using System;

namespace TrainingManagementSystem.Payments
{
    public class OnlinePaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("Processing simulated online payment...");
            Console.WriteLine("Amount: Rs. " + amount);

            Console.Write("Enter 1 for success or 2 for failure: ");
            string input = Console.ReadLine() ?? "";

            if (input == "1")
            {
                Console.WriteLine("Online payment successful.");
                return true;
            }

            Console.WriteLine("Online payment failed.");
            return false;
        }
    }
}
