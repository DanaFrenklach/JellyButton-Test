using JellyButton.Exam.Core.StateMachineBase;

namespace JellyButton.Exam.Game.StateMachine.Game
{
    /// <summary>
    /// The base class for the high-level game states
    /// </summary>
    public abstract class GameState : State<GameManager>
    {
        protected GameState(GameManager stateMachine) : base(stateMachine)
        {
        }
    }
}
