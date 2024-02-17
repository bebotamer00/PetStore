namespace PetStore.Core.Models
{
    public class WorkingHours
    {
        public int Id { get; set; }
        public int VetId { get; set; }
        public List<WeekDay> WeekDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public enum WeekDay
    {
        Saturday = 1,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
    }
}