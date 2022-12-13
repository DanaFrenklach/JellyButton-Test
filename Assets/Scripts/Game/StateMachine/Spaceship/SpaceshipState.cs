using JellyButton.Exam.Core.StateMachineBase;

namespace JellyButton.Exam.Game.StateMachine.Spaceship
{
    /// <summary>
    /// The base class for the spaceship states
    /// </summary>
    public abstract class SpaceshipState : State<SpaceshipController>
    {
        protected SpaceshipState(SpaceshipController stateMachine) : base(stateMachine)
        {
        }
    }
}
