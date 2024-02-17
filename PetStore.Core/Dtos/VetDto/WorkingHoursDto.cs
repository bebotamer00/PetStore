namespace PetStore.Core.Dtos.VetDto
{
    public class WorkingHoursDto
    {
        public List<WeekDay> WeekDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
