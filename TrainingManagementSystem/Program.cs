using System;
using TrainingManagementSystem.Services;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Sessions;

namespace TrainingManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            const int EXIT = 14;

            TrainingService trainingService = new TrainingService();
            LearnerService learnerService = new LearnerService();
            EnrollmentService enrollmentService =
                new EnrollmentService(trainingService, learnerService);
            PaymentService paymentService =
                new PaymentService(enrollmentService);
            ReportService reportService =
                new ReportService(trainingService, enrollmentService, paymentService);

            int choice = 0;

            while (choice != EXIT)
            {
                Console.WriteLine("\n===== TRAINING MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Display All Courses");
                Console.WriteLine("2. Search Courses");
                Console.WriteLine("3. Sort Courses by Fee");
                Console.WriteLine("4. Register a Learner");
                Console.WriteLine("5. Display All Learners");
                Console.WriteLine("6. Search for a Learner");
                Console.WriteLine("7. Enroll in a Course");
                Console.WriteLine("8. Display Enrollment Records");
                Console.WriteLine("9. Cancel an Enrollment");
                Console.WriteLine("10. Display Fee Breakdown");
                Console.WriteLine("11. Process a Simulated Payment");
                Console.WriteLine("12. View Payment History");
                Console.WriteLine("13. Display Reports");
                Console.WriteLine("14. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out choice) == false)
                {
                    Console.WriteLine("Please enter a valid number.");
                    choice = 0;
                    continue;
                }

                if (choice == 1)
                {
                    trainingService.DisplayCourses();
                }
                else if (choice == 2)
                {
                    Console.WriteLine("1. Search by Course Code");
                    Console.WriteLine("2. Search by Course Title");
                    Console.Write("Enter your choice: ");
                    string searchChoice = Console.ReadLine() ?? "";

                    if (searchChoice == "1")
                    {
                        Console.Write("Enter course code: ");
                        string code = Console.ReadLine() ?? "";
                        trainingService.SearchByCode(code);
                    }
                    else if (searchChoice == "2")
                    {
                        Console.Write("Enter course title: ");
                        string title = Console.ReadLine() ?? "";
                        trainingService.SearchByTitle(title);
                    }
                    else
                    {
                        Console.WriteLine("Invalid search choice.");
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("1. Ascending");
                    Console.WriteLine("2. Descending");
                    Console.Write("Enter your choice: ");
                    string sortInput = Console.ReadLine() ?? "";

                    if (sortInput == "1")
                    {
                        trainingService.SortByFee(true);
                    }
                    else if (sortInput == "2")
                    {
                        trainingService.SortByFee(false);
                    }
                    else
                    {
                        Console.WriteLine("Invalid sorting choice.");
                    }
                }
                else if (choice == 4)
                {
                    learnerService.RegisterLearner();
                }
                else if (choice == 5)
                {
                    learnerService.DisplayLearners();
                }
                else if (choice == 6)
                {
                    learnerService.SearchLearner();
                }
                else if (choice == 7)
                {
                    enrollmentService.CreateEnrollment();
                }
                else if (choice == 8)
                {
                    enrollmentService.DisplayEnrollments();
                }
                else if (choice == 9)
                {
                    enrollmentService.CancelEnrollment();
                }
                else if (choice == 10)
                {
                    FeeCalculator feeCalculator = new FeeCalculator();

                    Console.Write("Enter base fee: ");
                    string feeInput = Console.ReadLine() ?? "";

                    if (decimal.TryParse(feeInput, out decimal baseFee) == false
                        || baseFee < 0)
                    {
                        Console.WriteLine("Please enter a valid fee.");
                        continue;
                    }

                    Console.Write("Enter early-bird discount: ");
                    string earlyInput = Console.ReadLine() ?? "";

                    if (decimal.TryParse(earlyInput, out decimal earlyDiscount) == false
                        || earlyDiscount < 0)
                    {
                        Console.WriteLine("Please enter a valid discount.");
                        continue;
                    }

                    Console.Write("Enter group discount: ");
                    string groupInput = Console.ReadLine() ?? "";

                    if (decimal.TryParse(groupInput, out decimal groupDiscount) == false
                        || groupDiscount < 0)
                    {
                        Console.WriteLine("Please enter a valid discount.");
                        continue;
                    }

                    Console.Write("Enter session surcharge: ");
                    string surchargeInput = Console.ReadLine() ?? "";

                    if (decimal.TryParse(surchargeInput, out decimal surcharge) == false
                        || surcharge < 0)
                    {
                        Console.WriteLine("Please enter a valid surcharge.");
                        continue;
                    }

                    try
                    {
                        var result = feeCalculator.CalculateFee(
                            baseFee, earlyDiscount, groupDiscount, surcharge);

                        FeeSummary summary = new FeeSummary(
                            result.BaseFee,
                            earlyDiscount,
                            groupDiscount,
                            surcharge,
                            result.FinalFee);

                        feeCalculator.DisplayFeeBreakdown(summary);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (choice == 11)
                {
                    paymentService.ProcessPayment();
                }
                else if (choice == 12)
                {
                    paymentService.DisplayPaymentHistory();
                }
                else if (choice == 13)
                {
                    reportService.DisplayReports();
                }
                else if (choice == EXIT)
                {
                    Console.WriteLine("Thank you!");
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
    }
}
