
using System;
using TrainingManagementSystem.exs;
using TrainingManagementSystem.Models;
using System.Collections.Generic;

namespace TrainingManagementSystem.Services
{
    public class TrainingService
    {
        private Course[] courses =
        {
            new Course("C101", "CSharp", 5000, 30, 30, 10),
            new Course("C102", "Java", 4000, 25, 25, 5),
            new Course("C103", "Python", 3000, 20, 20, 8),
            new Course("C104", "HTML", 2000, 15, 15, 3),
            new Course("C105", "SQL", 2500, 10, 10, 2)
        };

        public void DisplayCourses()
        {
            Console.WriteLine(
                "\nCode\tTitle\tFee\tDays\tSeats\tEnrolled");

            for (int i = 0; i < courses.Length; i++)
            {
                DisplayCourse(courses[i]);
            }
        }

        public void SearchByCode(string code)
        {
            bool found = false;

            for (int i = 0; i < courses.Length; i++)
            {
                if (string.Equals(courses[i].CourseCode, code,
                    StringComparison.OrdinalIgnoreCase))
                {
                    DisplayCourseHeader();
                    DisplayCourse(courses[i]);
                    found = true;
                }
            }

            if (found == false)
            {
                throw new SearchByCodeException("INVALID");
            }
            
        }

        public void SearchByTitle(string title)
        {
            bool found = false;

            for (int i = 0; i < courses.Length; i++)
            {
                if (string.Equals(courses[i].Title, title,
                    StringComparison.OrdinalIgnoreCase))
                {
                    DisplayCourseHeader();
                    DisplayCourse(courses[i]);
                    found = true;
                }
            }

            if (found == false)
            {
                Console.WriteLine("Course not found.");
            }
        }

        public void SortByFee(bool ascending)
        {
            for (int i = 0; i < courses.Length - 1; i++)
            {
                for (int j = 0; j < courses.Length - 1 - i; j++)
                {
                    bool swap = false;

                    if (ascending && courses[j].Fee > courses[j + 1].Fee)
                    {
                        swap = true;
                    }

                    if (ascending == false &&
                        courses[j].Fee < courses[j + 1].Fee)
                    {
                        swap = true;
                    }

                    if (swap)
                    {
                        Course temp = courses[j];
                        courses[j] = courses[j + 1];
                        courses[j + 1] = temp;
                    }
                }
            }

            DisplayCourses();
        }

        public void DisplayAvailableSeats()
        {
            for (int i = 0; i < courses.Length; i++)
            {
                int available = courses[i].TotalSeats -
                                courses[i].EnrolledCount;

                Console.WriteLine(
                    courses[i].Title + " - " +
                    available + " seats available");
            }
        }

        public Course? FindCourse(string courseCode)
        {
            for (int i = 0; i < courses.Length; i++)
            {
                if (string.Equals(courses[i].CourseCode, courseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return courses[i];
                }
            }

            return null;

        }

        private void DisplayCourseHeader()
        {
            Console.WriteLine(
                "\nCode\tTitle\tFee\tDays\tSeats\tEnrolled");
        }

        private void DisplayCourse(Course course)
        {
            Console.WriteLine(
                course.CourseCode + "\t" +
                course.Title + "\t" +
                course.Fee + "\t" +
                course.DurationDays + "\t" +
                course.TotalSeats + "\t" +
                course.EnrolledCount);
        }

        public CourseSummary[] GetCourseSummaries()
        {
            CourseSummary[] summaries = new CourseSummary[courses.Length];

            for (int i = 0; i < courses.Length; i++)
            {
                int availableSeats =
                    courses[i].TotalSeats - courses[i].EnrolledCount;

                summaries[i] = new CourseSummary(
                    courses[i].CourseCode,
                    courses[i].Title,
                    courses[i].Fee,
                    availableSeats);
            }

            return summaries;
        }
        public List<Course> GetAllCourses()
        {
            return new List<Course>(courses);
        }

        public void LoadCourses(List<Course> savedCourses)
        {
            if (savedCourses != null && savedCourses.Count > 0)
            {
                courses = savedCourses.ToArray();
            }
        }

    }
}
