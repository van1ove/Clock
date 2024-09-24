using System.Collections;
using Project.Scripts.Models;
using Project.Scripts.Utility;
using Project.Scripts.Views;
using UnityEngine;

namespace Project.Scripts.Controllers
{
    public class ClockController : MonoBehaviour
    {
        private const int HoursOnClockFace = 12;

        private const float ClockFaceDivisionPrice = 30f;
        private const float FullCircleDegrees = 360f;
        
        private const float SecondsRotationAngle = 6f;
        private const float MinutesRotationAngle = 6f;
        private const float HoursRotationAngle = 0.5f;
        
        [SerializeField] private ClockView clockView;
        
        private float _secondsDegree;
        private float _minutesDegree;
        private float _hoursDegree;
        
        private Coroutine _clockUpdater;

        public TimeModel ClockModel { get; private set; }

        public void Init(TimeModel clockModel)
        {
            if(_clockUpdater != null)
                StopCoroutine(_clockUpdater);
            
            ClockModel = new TimeModel(clockModel.hours, clockModel.minutes, clockModel.seconds);
            SetStartClockValues();
            
            _clockUpdater = StartCoroutine(ClockTick());
        }

        private void SetStartClockValues()
        {
            _secondsDegree = -(FullCircleDegrees * ClockModel.seconds / TimeConst.SecondsPerMinute);
            _minutesDegree = -(FullCircleDegrees * ClockModel.minutes / TimeConst.MinutesPerHour);
            _hoursDegree = -(FullCircleDegrees * (ClockModel.hours % HoursOnClockFace) / HoursOnClockFace) - 
                           ClockFaceDivisionPrice * ClockModel.minutes / TimeConst.MinutesPerHour;
            
            clockView.MoveSecondsClockHand(_secondsDegree);
            clockView.MoveMinutesClockHand(_minutesDegree);
            clockView.MoveHoursClockHand(_hoursDegree);
        }

        private IEnumerator ClockTick()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(TimeConst.MoveDelay);
                
                SecondsTick();
                if (!(Mathf.Abs(_secondsDegree) >= FullCircleDegrees)) continue;
                
                _secondsDegree %= FullCircleDegrees;
                ClockModel.seconds %= TimeConst.SecondsPerMinute;
                MinutesTick();

                if (!(Mathf.Abs(_minutesDegree) >= FullCircleDegrees)) continue;
            
                _minutesDegree %= FullCircleDegrees;
                ClockModel.minutes %= TimeConst.MinutesPerHour;
                HoursTick();
            }
        }

        private void SecondsTick()
        {
            _secondsDegree -= SecondsRotationAngle;
            ClockModel.seconds++;
            clockView.MoveSecondsClockHand(_secondsDegree);
        }

        private void MinutesTick()
        {
            _minutesDegree -= MinutesRotationAngle;
            ClockModel.minutes++;
            clockView.MoveMinutesClockHand(_minutesDegree);
        }

        private void HoursTick()
        {
            _hoursDegree = (_hoursDegree - HoursRotationAngle) % HoursOnClockFace;
            ClockModel.hours++;
            ClockModel.hours %= TimeConst.HoursPerDay;
            clockView.MoveHoursClockHand(_hoursDegree);
        }
    }
}
