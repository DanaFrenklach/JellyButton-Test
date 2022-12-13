using JellyButton.Exam.Core.Data;

namespace JellyButton.Exam.Game.StateMachine.Game
{
    public class GameOverState : GameState
    {
        public GameOverState(GameManager stateMachine) : base(stateMachine)
        {
        }

        public override void Start()
        {
            base.Start();
            GameStateEvents.OnGameOver();
        }
    }
}