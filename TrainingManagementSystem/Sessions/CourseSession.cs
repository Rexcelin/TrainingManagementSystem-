using System;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Sessions
{
    public abstract class CourseSession
    {
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public SessionDuration Duration { get; set; }

        protected CourseSession(
            string courseCode,
            DateTime startDate,
            SessionDuration duration)
        {
            CourseCode = courseCode;
            StartDate = startDate;
            Duration = duration;
        }

        public abstract decimal CalculateSessionCharge();

        public virtual void DisplaySession()
        {
            Console.WriteLine("Course Code: " + CourseCode);
            Console.WriteLine("Start Date: " + StartDate.ToShortDateString());
            Console.WriteLine("Duration: " + Duration);
            Console.WriteLine("Session Charge: Rs. " +
                CalculateSessionCharge());
        }
    }
}
