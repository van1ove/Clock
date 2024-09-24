namespace Project.Scripts.Models
{
    public class TimeModel
    {
        public TimeModel(int hours, int minutes, int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }
        
        public int hours;
        public int minutes;
        public int seconds;

        public bool IsEqual(TimeModel model)
        {
            return hours == model.hours && minutes == model.minutes && seconds == model.seconds;
        }
    }
}