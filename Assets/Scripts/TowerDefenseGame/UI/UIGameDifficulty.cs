using TMPro;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UIGameDifficulty : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [Header("UI")] [SerializeField] private TextMeshProUGUI difficultyText;

        private void Awake()
        {
            gameController.DebugAssert();
            difficultyText.DebugAssert();
            difficultyText.gameObject.SetActive(false);

            OnGameDifficultyChanged(gameController.DifficultPercent);
        }

        private void OnEnable()
        {
            gameController.onGameDifficultyChanged.AddListener(OnGameDifficultyChanged);
        }

        private void OnDisable()
        {
            gameController.onGameDifficultyChanged.RemoveListener(OnGameDifficultyChanged);
        }

        private void OnGameDifficultyChanged(float difficultyPercent)
        {
            difficultyText.gameObject.SetActive(true);
            difficultyText.SetText($"Current Difficulty: {difficultyPercent * 100f}%");
        }
    }
}