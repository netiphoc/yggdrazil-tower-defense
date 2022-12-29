using System;
using Utilities;

namespace TowerDefenseGame
{
    public class WaveManager
    {
        public static event Action<int> OnWaveChanged;

        private int _wave;

        public int Wave
        {
            get => _wave;
            set
            {
                _wave = value;
                OnWaveChanged?.Invoke(_wave);
            }
        }

        public CountdownTimer WaveTimer { get; }

        public WaveManager(float updateThreshold)
        {
            WaveTimer = new CountdownTimer(updateThreshold);
        }

        public void StartWave(float duration)
        {
            WaveTimer.SetTime(duration);
        }

        public void UpdateWave()
        {
            WaveTimer.UpdateTimer();
        }
    }
}