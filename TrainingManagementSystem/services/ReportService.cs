using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class ReportService
    {
        private TrainingService trainingService;
        private EnrollmentService enrollmentService;
        private PaymentService paymentService;

        public ReportService(
            TrainingService trainingService,
            EnrollmentService enrollmentService,
            PaymentService paymentService)
        {
            this.trainingService = trainingService;
            this.enrollmentService = enrollmentService;
            this.paymentService = paymentService;
        }

        public void DisplayReports()
        {
            Console.WriteLine("\n===== TRAINING MANAGEMENT REPORTS =====");
            Console.WriteLine("1. Course Occupancy");
            Console.WriteLine("2. Expected Fees");
            Console.WriteLine("3. Payment Summary");
            Console.WriteLine("4. Course Enrollment Summary");
            Console.Write("Enter report choice: ");

            string input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Please enter a valid number.");
                return;
            }

            switch (choice)
            {
                case 1:
                    DisplayCourseOccupancy();
                    break;

                case 2:
                    DisplayExpectedFees();
                    break;

                case 3:
                    DisplayPaymentSummary();
                    break;

                case 4:
                    DisplayCourseEnrollmentSummary();
                    break;

                default:
                    Console.WriteLine("Invalid report choice.");
                    break;
            }
        }

        private void DisplayCourseOccupancy()
        {
            Console.WriteLine("\n===== COURSE OCCUPANCY =====");

            CourseSummary[] summaries =
                trainingService.GetCourseSummaries();

            foreach (CourseSummary course in summaries)
            {
                int totalSeats = course.AvailableSeats;

                // Find the actual course to get its total seats.
                Course? actualCourse =
                    trainingService.FindCourse(course.CourseCode);

                if (actualCourse == null)
                {
                    continue;
                }

                totalSeats = actualCourse.TotalSeats;
                int occupiedSeats = actualCourse.EnrolledCount;
                int availableSeats = totalSeats - occupiedSeats;

                decimal percentage = 0;

                if (totalSeats > 0)
                {
                    percentage =
                        (decimal)occupiedSeats / totalSeats * 100;
                }

                Console.WriteLine(
                    course.CourseCode + " - " + course.Title);
                Console.WriteLine("Total Seats: " + totalSeats);
                Console.WriteLine("Occupied Seats: " + occupiedSeats);
                Console.WriteLine("Available Seats: " + availableSeats);
                Console.WriteLine("Occupancy: " + percentage.ToString("F2") + "%");
                Console.WriteLine();
            }
        }

        private void DisplayExpectedFees()
        {
            decimal expectedFees =
                enrollmentService.GetExpectedFees();

            Console.WriteLine("\n===== EXPECTED FEES =====");
            Console.WriteLine(
                "Expected fees from active enrollments: Rs. " +
                expectedFees.ToString("F2"));
        }

        private void DisplayPaymentSummary()
        {
            List<Payment> payments = paymentService.GetPayments();

            int successfulCount = 0;
            int failedCount = 0;
            decimal totalCollected = 0;

            foreach (Payment payment in payments)
            {
                if (payment.Status == PaymentStatus.Successful)
                {
                    successfulCount++;
                    totalCollected += payment.Amount;
                }
                else if (payment.Status == PaymentStatus.Failed)
                {
                    failedCount++;
                }
            }

            Console.WriteLine("\n===== PAYMENT SUMMARY =====");
            Console.WriteLine("Successful Attempts: " + successfulCount);
            Console.WriteLine("Failed Attempts: " + failedCount);
            Console.WriteLine(
                "Total Collected: Rs. " + totalCollected.ToString("F2"));
        }

        private void DisplayCourseEnrollmentSummary()
        {
            Console.WriteLine("\n===== COURSE ENROLLMENT SUMMARY =====");

            CourseSummary[] summaries =
                trainingService.GetCourseSummaries();

            foreach (CourseSummary course in summaries)
            {
                Course? actualCourse =
                    trainingService.FindCourse(course.CourseCode);

                if (actualCourse == null)
                {
                    continue;
                }

                Console.WriteLine(
                    course.CourseCode + " - " + course.Title);

                Console.WriteLine(
                    "Active Enrollments: " + actualCourse.EnrolledCount);

                if (course.AvailableSeats == 0)
                {
                    Console.WriteLine("Course is full.");
                }
                else
                {
                    Console.WriteLine(
                        "Available Seats: " + course.AvailableSeats);
                }

                Console.WriteLine();
            }
        }
    }
}
