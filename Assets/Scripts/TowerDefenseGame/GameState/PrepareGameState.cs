namespace TowerDefenseGame.GameState
{
    public class PrepareGameState : State
    {
        public PrepareGameState(GameController gameController) : base(gameController)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            GameManager.InitializeGame();
        }
    }
}