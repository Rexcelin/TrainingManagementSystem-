
using System;

namespace TrainingManagementSystem.Payments
{
    public class ClassroomPaymentProcessor : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(decimal amount)
        {
            Console.WriteLine("Processing simulated classroom payment...");
            Console.WriteLine("Amount: Rs. " + amount);

            Console.Write("Enter 1 for success or 2 for failure: ");
            string input = Console.ReadLine() ?? "";

            if (input == "1")
            {
                Console.WriteLine("Classroom payment successful.");
                return true;
            }

            Console.WriteLine("Classroom payment failed.");
            return false;
        }
    }
}
