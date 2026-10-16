
namespace TrainingManagementSystem.Models
{
    public struct SessionDuration
    {
        public int Days { get; set; }

        public SessionDuration(int days)
        {
            Days = days;
        }

        public override string ToString()
        {
            return Days + " days";
        }
    }
}
