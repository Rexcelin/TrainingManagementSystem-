
using System;
using TrainingManagementSystem.Services;

namespace TrainingManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            const int EXIT = 13;

            TrainingService trainingService = new TrainingService();

            LearnerService learnerService = new LearnerService();

            EnrollmentService enrollmentService =
                new EnrollmentService(trainingService, learnerService);

            int choice = 0;

            while (choice != EXIT)
            {
                Console.WriteLine("\n===== TRAINING MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Display Courses");
                Console.WriteLine("2. Search Course by Code");
                Console.WriteLine("3. Search Course by Title");
                Console.WriteLine("4. Sort Courses by Fee");
                Console.WriteLine("5. Display Available Seats");
                Console.WriteLine("6. Register Learner");
                Console.WriteLine("7. Display All Learners");
                Console.WriteLine("8. Search Learner");
                Console.WriteLine("9. Enroll Learner");
                Console.WriteLine("10. Cancel Enrollment");
                Console.WriteLine("11. Display Enrollments");
                Console.WriteLine("12. Remove Learner");
                Console.WriteLine("13. Exit");
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
                    Console.Write("Enter course code: ");
                    string code = Console.ReadLine() ?? "";

                    trainingService.SearchByCode(code);
                }
                else if (choice == 3)
                {
                    Console.Write("Enter course title: ");
                    string title = Console.ReadLine() ?? "";

                    trainingService.SearchByTitle(title);
                }
                else if (choice == 4)
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
                else if (choice == 5)
                {
                    trainingService.DisplayAvailableSeats();
                }
                else if (choice == 6)
                {
                    learnerService.RegisterLearner();
                }
                else if (choice == 7)
                {
                    learnerService.DisplayLearners();
                }
                else if (choice == 8)
                {
                    learnerService.SearchLearner();
                }
                else if (choice == 9)
                {
                    enrollmentService.CreateEnrollment();
                }
                else if (choice == 10)
                {
                    enrollmentService.CancelEnrollment();
                }
                else if (choice == 11)
                {
                    enrollmentService.DisplayEnrollments();
                }
                else if (choice == 12)
                {
                    Console.Write("Enter learner ID to remove: ");
                    string learnerId = Console.ReadLine() ?? "";

                    if (enrollmentService.HasEnrollmentHistoryForLearner(
                        learnerId))
                    {
                        Console.WriteLine(
                            "Cannot remove this learner because enrollment history exists.");
                    }
                    else if (learnerService.RemoveLearner(learnerId))
                    {
                        Console.WriteLine("Learner removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Learner not found.");
                    }
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
