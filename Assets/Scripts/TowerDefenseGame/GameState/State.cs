using Utilities;

namespace TowerDefenseGame.GameState
{
    public abstract class State
    {
        public GameController GameController { get; }

        protected State(GameController gameController)
        {
            GameController = gameController;
        }

        public virtual void OnEnter()
        {
            this.Log("OnEnter");
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnExit()
        {
            this.Log("OnExit");
        }
    }
}