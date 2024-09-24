using System.Collections;
using Project.Scripts.Models;
using Project.Scripts.Utility;
using Project.Scripts.Views;
using UnityEngine;

namespace Project.Scripts.Controllers
{
    public class DigitalClockController : MonoBehaviour
    {
        [SerializeField] private DigitalClockView clockView;
        private Coroutine _clockUpdater;
        
        public TimeModel ClockModel { get; private set; }

        public void Init(TimeModel clockModel)
        {
            if(_clockUpdater != null)
                StopCoroutine(_clockUpdater);
            
            ClockModel = new TimeModel(clockModel.hours, clockModel.minutes, clockModel.seconds);
            clockView.UpdateSeconds(ClockModel.seconds);
            clockView.UpdateMinutes(ClockModel.minutes);
            clockView.UpdateHours(ClockModel.hours);
            
            _clockUpdater = StartCoroutine(ClockTick());
        }

        private IEnumerator ClockTick()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(TimeConst.MoveDelay);
                ClockModel.seconds++;

                if (ClockModel.seconds < TimeConst.SecondsPerMinute)
                {
                    clockView.UpdateSeconds(ClockModel.seconds);
                    continue;
                }

                ClockModel.seconds %= TimeConst.SecondsPerMinute;
                clockView.UpdateSeconds(ClockModel.seconds);
                ClockModel.minutes++;

                if (ClockModel.minutes < TimeConst.MinutesPerHour)
                {
                    clockView.UpdateMinutes(ClockModel.minutes);
                    continue;
                }
                
                ClockModel.minutes %= TimeConst.MinutesPerHour;
                clockView.UpdateMinutes(ClockModel.minutes);
                ClockModel.hours++;
                ClockModel.hours %= TimeConst.HoursPerDay;
                clockView.UpdateHours(ClockModel.hours);
            }
        }
    }
}
