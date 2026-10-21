
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;
using System.Collections.Generic;

namespace TrainingManagementSystem.Services
{
    public class LearnerService
    {
        private Dictionary<string, Learner> learners =
            new Dictionary<string, Learner>(
                StringComparer.OrdinalIgnoreCase);

        public void RegisterLearner()
        {
            Console.Write("Enter learner ID: ");
            string learnerId = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(learnerId))
            {
                Console.WriteLine("Learner ID cannot be empty.");
                return;
            }

            if (learners.ContainsKey(learnerId))
            {
                Console.WriteLine("Learner ID already exists.");
                return;
            }

            Console.Write("Enter learner name: ");
            string name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Learner name cannot be empty.");
                return;
            }

            Console.Write("Enter contact number (10 digits): ");
            string contactNumber = Console.ReadLine() ?? "";

            if (IsValidContact(contactNumber) == false)
            {
                Console.WriteLine(
                    "Invalid contact number. Enter exactly 10 digits.");
                return;
            }

            Console.Write("Enter email: ");
            string email = Console.ReadLine() ?? "";

            Learner learner = new Learner(
                learnerId, name, contactNumber, email);

            learners.Add(learnerId, learner);

            Console.WriteLine("Learner registered successfully.");
        }

        private bool IsValidContact(string contactNumber)
        {
            if (contactNumber.Length != 10)
            {
                return false;
            }

            for (int i = 0; i < contactNumber.Length; i++)
            {
                if (contactNumber[i] < '0' ||
                    contactNumber[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }

        public void DisplayLearners()
        {
            if (learners.Count == 0)
            {
                Console.WriteLine("No learners registered.");
                return;
            }

            Console.WriteLine(
                "\nLearner ID\tName\tContact\tEmail");

            foreach (Learner learner in learners.Values)
            {
                Console.WriteLine(
                    learner.LearnerId + "\t\t" +
                    learner.Name + "\t" +
                    learner.ContactNumber + "\t" +
                    learner.Email);
            }
        }

        public Learner? FindLearner(string learnerId)
        {
            if (learners.TryGetValue(learnerId, out Learner? learner))
            {
                return learner;
            }

            return null;
        }

        public void SearchLearner()
        {
            Console.Write("Enter learner ID: ");
            string learnerId = Console.ReadLine() ?? "";

            Learner? learner = FindLearner(learnerId);

            if (learner == null)
            {
                Console.WriteLine("Learner not found.");
            }
            else
            {
                Console.WriteLine("\nLearner ID: " + learner.LearnerId);
                Console.WriteLine("Name: " + learner.Name);
                Console.WriteLine("Contact: " + learner.ContactNumber);
                Console.WriteLine("Email: " + learner.Email);
            }
        }

        public bool RemoveLearner(string learnerId)
        {
            return learners.Remove(learnerId);
        }
        public List<Learner> GetAllLearners()
        {
            return new List<Learner>(learners.Values);
        }

        public void LoadLearners(List<Learner> savedLearners)
        {
            learners.Clear();

            foreach (Learner learner in savedLearners)
            {
                learners[learner.LearnerId] = learner;
            }
        }
    }
}
