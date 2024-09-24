using UnityEngine;
using TMPro;

namespace Project.Scripts.Views
{
    public class DigitalClockView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hoursTMP;
        [SerializeField] private TextMeshProUGUI minutesTMP;
        [SerializeField] private TextMeshProUGUI secondsTMP;

        public void UpdateSeconds(int seconds) => secondsTMP.text = ConvertValueForTime(seconds);

        public void UpdateMinutes(int minutes) => minutesTMP.text = ConvertValueForTime(minutes);

        public void UpdateHours(int hours) => hoursTMP.text = ConvertValueForTime(hours);

        private string ConvertValueForTime(int value) => value >= 10 ? value.ToString() : "0" + value;
    }
}