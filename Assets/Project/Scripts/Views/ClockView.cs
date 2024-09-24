using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Views
{
    public class ClockView : MonoBehaviour
    {
        private const float RotationDuration = 0.5f;
        [SerializeField] private Image hoursImage;
        [SerializeField] private Image minutesImage;
        [SerializeField] private Image secondsImage;

        public void MoveSecondsClockHand(float secondsDegree) =>
            secondsImage.transform.DORotate(new Vector3(0, 0, secondsDegree), RotationDuration);
        
        public void MoveMinutesClockHand(float minutesDegree) => 
            minutesImage.transform.DORotate(new Vector3(0, 0, minutesDegree), RotationDuration);
        
        public void MoveHoursClockHand(float hoursDegree) => 
            hoursImage.transform.DORotate(new Vector3(0, 0, hoursDegree), RotationDuration);
    }
}