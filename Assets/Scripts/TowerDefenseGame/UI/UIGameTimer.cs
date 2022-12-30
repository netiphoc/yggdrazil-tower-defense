using TMPro;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UIGameTimer : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField,
         Tooltip("[Format]\n" +
                 "Time percent: {time_percent}\n" +
                 "Time: {time}\n" +
                 "Time Length: {time_length}")]
        private TextMeshProUGUI timerText;

        private void Awake()
        {
            timerText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            CountdownTimer.OnCountdownUpdated += OnCountdownUpdated;
        }

        private void OnDisable()
        {
            CountdownTimer.OnCountdownUpdated -= OnCountdownUpdated;
        }

        private void OnCountdownUpdated(float time, float timeLength)
        {
            timerText.gameObject.SetActive(true);

            var timeFactor = time > 0f ? time / timeLength : 1f;
            var timePercent = timeFactor * 100f;
            var textFormat = timerText.text;
            textFormat = textFormat.Replace("{time_percent}", $"{timePercent:F}")
                .Replace("{time}", $"{time}")
                .Replace("{time_length}", $"{timeLength}");

            timerText.SetText($"Next Wave: {time}s");
        }
    }
}