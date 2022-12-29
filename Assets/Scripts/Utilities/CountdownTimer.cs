using System;
using UnityEngine;

namespace Utilities
{
    public class CountdownTimer
    {
        public static event Action<float, float> OnCountdownUpdated;

        public float TimeLength { get; private set; }

        private float _timer;

        public float Timer
        {
            get => _timer;
            private set
            {
                _timer = value;
                OnCountdownUpdated?.Invoke(value, TimeLength);
            }
        }

        public float UpdateThreshold { get; set; }
        public bool IsCountdownOver { get; private set; }

        private float _currentUpdateThreshold;

        public CountdownTimer(float updateThreshold = 1f)
        {
            UpdateThreshold = updateThreshold;
            _currentUpdateThreshold = updateThreshold;
        }

        public void SetTime(float time)
        {
            TimeLength = time;
            Timer = time;
            IsCountdownOver = false;
        }

        public void UpdateTimer()
        {
            if (IsCountdownOver) return;
            _currentUpdateThreshold -= Time.deltaTime;
            if (_currentUpdateThreshold > 0f) return;
            _currentUpdateThreshold = UpdateThreshold;
            Timer -= UpdateThreshold;
            if (Timer > 0f) return;
            Timer = 0f;
            IsCountdownOver = true;
        }
    }
}