
namespace TrainingManagementSystem.Utilities
{
    public class InputValidator
    {
        public bool TryGetLearnerId(
            string input, out string learnerId)
        {
            learnerId = input.Trim();

            if (learnerId == "")
            {
                return false;
            }

            return true;
        }
    }
}
