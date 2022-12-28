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
        public GameManager GameManager => gameManager;

        private State _gameState;

        private void Awake()
        {
            gameManager.DebugAssert();
        }

        private void Start()
        {
            SetState(new InGameState(this, enemyPerSpawn, spawnDelay));
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
    }
}