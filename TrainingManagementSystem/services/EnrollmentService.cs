
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class EnrollmentService
    {
        private List<Enrollment> enrollments =
            new List<Enrollment>();

        private TrainingService trainingService;
        private LearnerService learnerService;
        private int nextEnrollmentId = 1;

        public EnrollmentService(TrainingService trainingService,
            LearnerService learnerService)
        {
            this.trainingService = trainingService;
            this.learnerService = learnerService;
        }

        public void CreateEnrollment()
        {
            Console.Write("Enter learner ID: ");
            string learnerId = Console.ReadLine()?? "";

            Learner? learner = learnerService.FindLearner(learnerId);

            if (learner == null)
            {
                Console.WriteLine("Learner does not exist.");
                return;
            }

            Console.Write("Enter course code: ");
            string courseCode = Console.ReadLine() ?? "";

            Course? course = trainingService.FindCourse(courseCode);

            if (course == null)
            {
                Console.WriteLine("Course does not exist.");
                return;
            }

            for (int i = 0; i < enrollments.Count; i++)
            {
                if (string.Equals(enrollments[i].LearnerId, learnerId,
                        StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(enrollments[i].CourseCode, courseCode,
                        StringComparison.OrdinalIgnoreCase) &&
                    enrollments[i].Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine(
                        "Learner is already actively enrolled in this course.");
                    return;
                }
            }

            int availableSeats = course.TotalSeats - course.EnrolledCount;

            if (availableSeats <= 0)
            {
                Console.WriteLine("No seats are available.");
                return;
            }

            decimal baseFee = course.Fee;
            decimal discountAmount = 0;
            decimal finalFee = baseFee - discountAmount;

            Enrollment enrollment = new Enrollment(
                nextEnrollmentId,
                learner.LearnerId,
                course.CourseCode,
                DateTime.Now,
                baseFee,
                discountAmount,
                finalFee,
                EnrollmentStatus.Active);

            enrollments.Add(enrollment);

            course.EnrolledCount++;
            nextEnrollmentId++;

            Console.WriteLine("\nEnrollment successful!");
            Console.WriteLine("Enrollment ID: " + enrollment.EnrollmentId);
            Console.WriteLine("Learner: " + learner.Name);
            Console.WriteLine("Course: " + course.Title);
            Console.WriteLine("Base fee: " + enrollment.BaseFee);
            Console.WriteLine("Discount: " + enrollment.DiscountAmount);
            Console.WriteLine("Final fee: " + enrollment.FinalFee);
            Console.WriteLine("Status: " + enrollment.Status);
        }

        public void CancelEnrollment()
        {
            Console.Write("Enter enrollment ID: ");
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int enrollmentId) == false)
            {
                Console.WriteLine("Invalid enrollment ID.");
                return;
            }

            for (int i = 0; i < enrollments.Count; i++)
            {
                if (enrollments[i].EnrollmentId == enrollmentId)
                {
                    if (enrollments[i].Status == EnrollmentStatus.Cancelled)
                    {
                        Console.WriteLine(
                            "This enrollment is already cancelled.");
                        return;
                    }

                    if (enrollments[i].Status != EnrollmentStatus.Active)
                    {
                        Console.WriteLine(
                            "Only active enrollments can be cancelled.");
                        return;
                    }

                    Course? course = trainingService.FindCourse(
                        enrollments[i].CourseCode);

                    if (course != null && course.EnrolledCount > 0)
                    {
                        course.EnrolledCount--;
                    }

                    enrollments[i].Status = EnrollmentStatus.Cancelled;

                    Console.WriteLine("Enrollment cancelled successfully.");
                    Console.WriteLine(
                        "The enrollment record has been preserved.");
                    return;
                }
            }

            Console.WriteLine("Enrollment not found.");
        }

        public void DisplayEnrollments()
        {
            if (enrollments.Count == 0)
            {
                Console.WriteLine("No enrollment records found.");
                return;
            }

            Console.WriteLine(
                "\nID\tLearner\tCourse\tDate\t\tBase Fee\tDiscount\tFinal Fee\tStatus");

            for (int i = 0; i < enrollments.Count; i++)
            {
                Console.WriteLine(
                    enrollments[i].EnrollmentId + "\t" +
                    enrollments[i].LearnerId + "\t" +
                    enrollments[i].CourseCode + "\t" +
                    enrollments[i].EnrollmentDate.ToShortDateString() + "\t" +
                    enrollments[i].BaseFee + "\t\t" +
                    enrollments[i].DiscountAmount + "\t\t" +
                    enrollments[i].FinalFee + "\t\t" +
                    enrollments[i].Status);
            }
        }

        public bool HasEnrollmentHistoryForLearner(string learnerId)
        {
            for (int i = 0; i < enrollments.Count; i++)
            {
                if (string.Equals(enrollments[i].LearnerId, learnerId,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public Enrollment? FindEnrollment(int enrollmentId)
        {
            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.EnrollmentId == enrollmentId)
                {
                    return enrollment;
                }
            }

            return null;
        }

        public decimal GetExpectedFees()
        {
            decimal totalFees = 0;

            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.Status == EnrollmentStatus.Active)
                {
                    totalFees = totalFees + enrollment.FinalFee;
                }
            }

            return totalFees;
        }
    }
}
