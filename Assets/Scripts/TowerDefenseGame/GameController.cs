using TowerDefenseGame.GameState;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace TowerDefenseGame
{
    public class GameController : MonoBehaviour
    {
        public enum GameStateType
        {
            Prepare,
            InGame,
            GameOver,
        }

        public UnityEvent<GameStateType> onGameStateChanged;
        public UnityEvent<float> onGameDifficultyChanged;

        private GameStateType _currentGameState;

        public GameStateType CurrentGameState
        {
            get => _currentGameState;
            set
            {
                _currentGameState = value;
                onGameStateChanged?.Invoke(value);
            }
        }

        [SerializeField] private GameManager gameManager;

        [Header("Game Config")] [SerializeField]
        private int enemyPerSpawn = 10;

        [SerializeField] private float waveDuration = 60f;
        [SerializeField] private float enemySpawnDelay = 1f;

        [SerializeField, Tooltip("In percent %"), Range(0, 100f)]
        private float difficultyIncreasePerWave = 0.5f;

        public GameManager GameManager => gameManager;
        private State _gameState;

        private float _difficultPercent;

        public float DifficultPercent
        {
            get => _difficultPercent;
            private set
            {
                _difficultPercent = value;
                onGameDifficultyChanged?.Invoke(value);
            }
        }

        private void Awake()
        {
            gameManager.DebugAssert();
            ConvertDifficultyInPercent();
        }

        private void Start()
        {
            SetState(GameStateType.Prepare);
        }

        private void Update()
        {
            _gameState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            _gameState?.OnFixedUpdate();
        }

        #region GameState

        public void SetState(State gameState)
        {
            _gameState?.OnExit();
            _gameState = gameState;
            _gameState.OnEnter();
        }

        public void SetState(GameStateType gameStateType)
        {
            CurrentGameState = gameStateType;

            switch (gameStateType)
            {
                case GameStateType.Prepare:
                    SetState(new PrepareGameState(this));
                    break;
                case GameStateType.InGame:
                    SetState(new InGameState(this,
                        enemyPerSpawn, waveDuration, enemySpawnDelay, difficultyIncreasePerWave));
                    break;
                case GameStateType.GameOver:
                    break;
            }
        }

        #endregion

        #region Game Difficcultiy

        private void ConvertDifficultyInPercent()
        {
            difficultyIncreasePerWave *= 0.01f;
        }

        public void SetDifficulty(float newDifficulty)
        {
            DifficultPercent = newDifficulty;
        }

        public void ResetDifficulty()
        {
            DifficultPercent = 0f;
        }

        #endregion
    }
}