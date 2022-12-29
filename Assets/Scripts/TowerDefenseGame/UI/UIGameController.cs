using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UIGameController : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private Button readyButton;
        [SerializeField] private Button restartButton;

        private void Awake()
        {
            gameController.DebugAssert();
            readyButton.DebugAssert();

            readyButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(false);

            readyButton.onClick.AddListener(StartGame);
            restartButton.onClick.AddListener(RestartGame);
        }

        private void StartGame()
        {
            var gameState = GameController.GameStateType.InGame;
            UpdateButtonState(gameState);
            gameController.SetState(gameState);
        }

        private void RestartGame()
        {
            var gameState = GameController.GameStateType.Prepare;
            UpdateButtonState(gameState);
            gameController.SetState(gameState);
        }

        private void UpdateButtonState(GameController.GameStateType gameStateType)
        {
            readyButton.gameObject.SetActive(gameStateType == GameController.GameStateType.Prepare);
            restartButton.gameObject.SetActive(gameStateType == GameController.GameStateType.InGame);
        }
    }
}