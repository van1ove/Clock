namespace Project.Scripts.Models
{
    public class TimeDto
    {
        public int dayOfWeek;
        public string dayOfWeekName;
        public int day;
        public int month;
        public string monthName;
        public int year;
        public int hours;
        public int minutes;
        public int seconds;
        public int millis;
        public string fullDate;
        public string timeZone;
        public string status;

        public TimeModel ToTimeModel() => new TimeModel(hours, minutes, seconds);
    }
}