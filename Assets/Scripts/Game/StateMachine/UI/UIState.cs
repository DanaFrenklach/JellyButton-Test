using JellyButton.Exam.Core.StateMachineBase;

namespace JellyButton.Exam.Game.StateMachine.UI
{
    /// <summary>
    /// The base class for the UI states.
    /// </summary>
    public class UIState : State<UIManager>
    {
        protected UIState(UIManager stateMachine) : base(stateMachine)
        {
        }
    }
}
