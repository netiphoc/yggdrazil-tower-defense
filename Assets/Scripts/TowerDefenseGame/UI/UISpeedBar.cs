using TMPro;
using TowerDefenseGame.GameEntity;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UISpeedBar : MonoBehaviour
    {
        [SerializeField] private LivingEntity livingEntity;
        [Header("UI")] [SerializeField] private TextMeshProUGUI speedText;

        private void Awake()
        {
            speedText.DebugAssert();
        }

        private void OnEnable()
        {
            livingEntity.onSpeedChanged.AddListener(OnSpeedChanged);
        }

        private void OnDisable()
        {
            livingEntity.onSpeedChanged.RemoveAllListeners();
        }

        private void OnSpeedChanged(float value)
        {
            speedText.SetText($"Speed: {value:F}");
        }
    }
}