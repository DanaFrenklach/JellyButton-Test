using JellyButton.Exam.Core.Data;

namespace JellyButton.Exam.Game.StateMachine.Game
{
    public class InGameState : GameState
    {
        public InGameState(GameManager stateMachine) : base(stateMachine)
        {
        }

        public override void Start()
        {
            base.Start();
            GameStateEvents.OnInGame();
        }
    }
}