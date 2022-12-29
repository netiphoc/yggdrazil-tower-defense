using TMPro;
using UnityEngine;

namespace TowerDefenseGame.UI
{
    public class UIGameWave : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField,
         Tooltip("[Format]\n" +
                 "Current Wave: {wave}")]
        private TextMeshProUGUI waveText;

        private void Awake()
        {
            waveText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            WaveManager.OnWaveChanged += OnWaveChanged;
        }

        private void OnDisable()
        {
            WaveManager.OnWaveChanged -= OnWaveChanged;
        }

        private void OnWaveChanged(int wave)
        {
            waveText.gameObject.SetActive(true);

            var textFormat = waveText.text;
            textFormat = textFormat.Replace("{wave}", $"{wave}");

            waveText.SetText($"Wave: {wave}");
        }
    }
}