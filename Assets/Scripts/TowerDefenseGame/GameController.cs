using TowerDefenseGame.GameState;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        [Header("Game Config")] [SerializeField]
        private int enemyPerSpawn = 10;

        [SerializeField] private float spawnDelay = 60f;
        [SerializeField] private float difficultyIncreasePerWave = 0.5f;

        public GameManager GameManager => gameManager;
        public float DifficultPercent { get; private set; }

        private State _gameState;

        private void Awake()
        {
            gameManager.DebugAssert();
        }

        private void Start()
        {
            SetState(new InGameState(this, enemyPerSpawn, spawnDelay, difficultyIncreasePerWave));
        }

        private void Update()
        {
            _gameState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            _gameState?.OnFixedUpdate();
        }

        public void SetState(State gameState)
        {
            _gameState?.OnExit();
            _gameState = gameState;
            _gameState.OnEnter();
        }

        public void InCreaseDifficulty(float percent)
        {
            DifficultPercent += percent;
        }
    }
}