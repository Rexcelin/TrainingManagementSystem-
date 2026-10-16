
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Payments;

namespace TrainingManagementSystem.Services
{
    public class PaymentService
    {
        private List<Payment> payments = new List<Payment>();
        private EnrollmentService enrollmentService;
        private int nextPaymentId = 1;

        public PaymentService(EnrollmentService enrollmentService)
        {
            this.enrollmentService = enrollmentService;
        }

        public void ProcessPayment()
        {
            Console.Write("Enter Enrollment ID: ");
            string input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out int enrollmentId))
            {
                Console.WriteLine("Invalid enrollment ID.");
                return;
            }

            Enrollment? enrollment =
                enrollmentService.FindEnrollment(enrollmentId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            if (enrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("Cannot pay for a cancelled enrollment.");
                return;
            }

            if (enrollment.PaymentStatus == PaymentStatus.Successful)
            {
                Console.WriteLine("This enrollment has already been paid.");
                return;
            }

            Console.WriteLine("1. Online Payment");
            Console.WriteLine("2. Classroom Payment");
            Console.Write("Choose payment method: ");

            string choice = Console.ReadLine() ?? "";
            IPaymentProcessor processor;

            if (choice == "1")
            {
                processor = new OnlinePaymentProcessor();
            }
            else if (choice == "2")
            {
                processor = new ClassroomPaymentProcessor();
            }
            else
            {
                Console.WriteLine("Invalid payment method.");
                return;
            }

            bool success = processor.ProcessPayment(enrollment.FinalFee);

            PaymentStatus paymentStatus;

            if (success)
            {
                paymentStatus = PaymentStatus.Successful;
                enrollment.PaymentStatus = PaymentStatus.Successful;
            }
            else
            {
                paymentStatus = PaymentStatus.Failed;
            }

            Payment payment = new Payment(
                nextPaymentId,
                enrollmentId,
                enrollment.FinalFee,
                DateTime.Now,
                paymentStatus);

            payments.Add(payment);
            nextPaymentId++;

            Console.WriteLine("Payment attempt recorded.");
            Console.WriteLine("Payment Status: " + paymentStatus);
        }

        public void DisplayPaymentHistory()
        {
            Console.WriteLine("\n===== PAYMENT HISTORY =====");

            if (payments.Count == 0)
            {
                Console.WriteLine("No payment attempts found.");
                return;
            }

            foreach (Payment payment in payments)
            {
                Console.WriteLine(
                    "Payment ID: " + payment.PaymentId +
                    " | Enrollment ID: " + payment.EnrollmentId +
                    " | Amount: Rs. " + payment.Amount +
                    " | Date: " + payment.PaymentDate +
                    " | Status: " + payment.Status);
            }
        }

        public List<Payment> GetPayments()
        {
            return payments;
        }
    }
}
