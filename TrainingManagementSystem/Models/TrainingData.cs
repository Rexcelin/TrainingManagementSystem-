using System.Collections.Generic;

namespace TrainingManagementSystem.Models
{
    public class TrainingData
    {
        public List<Course> Courses { get; set; } = new List<Course>();

        public List<Learner> Learners { get; set; } = new List<Learner>();

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}